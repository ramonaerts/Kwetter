using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ModerationService.DAL;
using ModerationService.Entities;
using ModerationService.Messages.Broker;
using ModerationService.Models;
using MongoDB.Driver;
using Shared.Messaging;
using Tweet = ModerationService.Models.Tweet;

namespace ModerationService.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IMongoCollection<Entities.Tweet> _tweets;
        private readonly IMongoCollection<User> _users;

        public ModerationService(IModerationContext context, IMessagePublisher messagePublisher, IMapper mapper)
        {
            _messagePublisher = messagePublisher;
            _mapper = mapper;

            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Entities.Tweet>("Tweets");
            _users = database.GetCollection<User>("Users");
        }

        public async Task<bool> ApproveProfanityTweet(string tweetId)
        {
            var tweet = _tweets.Find(t => t.Id == tweetId).FirstOrDefault();

            if (tweet == null) return false;

            tweet.TweetStatus = Status.Approved;
            await _tweets.ReplaceOneAsync(t => t.Id == tweetId, tweet);

            return true;
        }

        public async Task<bool> UnApproveProfanityTweet(string tweetId)
        {
            var tweet = _tweets.Find(t => t.Id == tweetId).FirstOrDefault();

            if (tweet == null) return false;

            tweet.TweetStatus = Status.Unapproved;
            await _tweets.ReplaceOneAsync(t => t.Id == tweetId, tweet);

            await _messagePublisher.PublishMessageAsync("UnApproveTweetMessage", new { TweetId = tweetId });

            return true;
        }

        public async Task<bool> UpgradeUserToModerator(string userId)
        {
            var user = _users.Find(u => u.Id == userId).FirstOrDefault();

            if (user == null) return false;

            await _messagePublisher.PublishMessageAsync("UpgradeUserToAdminMessage", new { Id = user.Id });

            return true;
        }

        public List<Tweet> GetProfanityTweetsByStatus(Status status)
        {
            var tweets = _tweets.Find(t => t.TweetStatus == status).ToList();

            var tweetModels = _mapper.Map<List<Tweet>>(tweets);

            foreach (var tweet in tweetModels)
            {
                tweet.User = _users.Find(u => u.Id == tweet.UserId).FirstOrDefault();
            }

            tweetModels = tweetModels.OrderByDescending(x => x.TimeStamp).ToList();

            return tweetModels;
        }

        public async Task AddProfanityTweet(NewProfanityTweetMessage message)
        {
            var tweet = new Entities.Tweet
            {
                TweetDateTime = message.TweetDateTime,
                Id = message.Id,
                UserId = message.UserId,
                TweetContent = message.TweetContent,
                TweetStatus = Status.Pending
            };

            await _tweets.InsertOneAsync(tweet);
        }

        public async Task AddUser(NewProfileMessage message)
        {
            var user = new User
            {
                Id = message.Id,
                Nickname = message.Nickname,
                Username = message.Username,
                Image = message.Image
            };

            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUser(ProfileChangedMessage message)
        {
            var user = _users.Find(u => u.Id == message.Id).FirstOrDefault();

            user.Nickname = message.Nickname;

            await _users.ReplaceOneAsync(u => u.Id == message.Id, user);
        }

        public async Task UpdateUserImage(ProfileImageChangedMessage message)
        {
            var user = _users.Find(u => u.Id == message.Id).FirstOrDefault();

            user.Image = message.Image;

            await _users.ReplaceOneAsync(u => u.Id == message.Id, user);
        }

        public async Task ForgetUser(ForgetUserMessage message)
        {
            await _users.DeleteOneAsync(u => u.Id == message.Id);
            await _tweets.DeleteManyAsync(t => t.UserId == message.Id);
        }

        public async Task DeleteTweet(string tweetId)
        {
            await _tweets.DeleteOneAsync(t => t.Id == tweetId);
        }
    }
}
