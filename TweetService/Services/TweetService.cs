using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Shared.Messaging;
using TweetService.DAL;
using TweetService.Messages;
using TweetService.Messages.Broker;
using TweetService.Models;

namespace TweetService.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IMongoCollection<Entities.Tweet> _tweets;
        private readonly IMongoCollection<Entities.User> _users;

        public TweetService(ITweetContext context, IMapper mapper, IMessagePublisher messagePublisher)
        {
            _mapper = mapper;
            _messagePublisher = messagePublisher;

            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Entities.Tweet>("Tweets");
            _users = database.GetCollection<Entities.User>("Users");
        }

        public List<Tweet> GetTweets(string id)
        {
            var tweets = _tweets.Find(t => t.UserId == id).ToList();

            var user = _users.Find(u => u.Id == id).FirstOrDefault();

            var tweetModels = _mapper.Map<List<Tweet>>(tweets);

            foreach (var tweet in tweetModels)
            {
                tweet.User = user;
            }

            tweetModels = tweetModels.OrderByDescending(x => x.TweetDateTime).ToList();

            return tweetModels;
        }

        public void AddUser(NewProfileMessage message)
        {
            var user = new Entities.User
            {
                Id = message.Id,
                Nickname = message.Nickname,
                Username = message.Username,
                Image = message.Image
            };

            _users.InsertOne(user);
        }

        public void UpdateUser(ProfileChangedMessage message)
        {
            var user = _users.Find(u => u.Id == message.Id).FirstOrDefault();

            user.Nickname = message.Nickname;

            _users.ReplaceOne(u => u.Id == message.Id, user);
        }

        public void UpdateUserImage(ProfileImageChangedMessage message)
        {
            var user = _users.Find(u => u.Id == message.Id).FirstOrDefault();

            user.Image = message.Image;

            _users.ReplaceOne(u => u.Id == message.Id, user);
        }

        public Entities.Tweet GetTweet()
        {
            return _tweets.Find(t => t.Id == "1").FirstOrDefault();
        }

        public async Task CreateTweet(string id, string tweetContent)
        {
            var tweet = new Entities.Tweet
            {
                TweetDateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                UserId = id,
                TweetContent = tweetContent
            };

            await _messagePublisher.PublishMessageAsync("NewPostedTweetMessage", new { TweetDateTime = tweet.TweetDateTime, Id = tweet.Id, UserId = tweet.UserId, TweetContent = tweet.TweetContent });

            _tweets.InsertOne(tweet);
        }
    }
}
