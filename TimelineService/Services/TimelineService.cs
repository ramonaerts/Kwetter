using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using TimelineService.DAL;
using TimelineService.Entities;
using TimelineService.Messages.Broker;

namespace TimelineService.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Tweet> _tweets;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Follow> _follows;

        public TimelineService(ITimelineContext context, IMapper mapper)
        {
            _mapper = mapper;

            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Tweet>("Tweets");
            _users = database.GetCollection<User>("Users");
            _follows = database.GetCollection<Follow>("Followings");
        }

        public List<Models.Tweet> GetUserTimeline(string userId)
        {
            var followings = GetFollowings(userId);

            var tweets = new List<Tweet>();

            foreach (var follow in followings)
            {
                tweets.AddRange(_tweets.Find(t => t.UserId == follow.Following).ToList());
            }

            var tweetModels = _mapper.Map<List<Models.Tweet>>(tweets);

            foreach (var tweet in tweetModels)
            {
                tweet.User = _users.Find(u => u.Id == tweet.UserId).FirstOrDefault();

                tweet.OwnTweet = false;
            }

            tweetModels = tweetModels.OrderByDescending(x => x.TweetDateTime).ToList();

            return tweetModels;
        }

        public List<Follow> GetFollowings(string userId)
        {
            return _follows.Find(f => f.Follower == userId).ToList();
        }

        public async Task AddTweet(NewPostedTweetMessage message)
        {
            var tweet = new Tweet
            {
                TweetDateTime = message.TweetDateTime,
                Id = message.Id,
                UserId = message.UserId,
                TweetContent = message.TweetContent
            };

            await _tweets.InsertOneAsync(tweet);
        }

        public async Task FollowUser(AddFollowerMessage message)
        {
            var follow = new Follow
            {
                Id = message.Id,
                Follower = message.FollowerId,
                Following = message.FollowingId
            };

            await _follows.InsertOneAsync(follow);
        }

        public async Task UnFollowUser(RemoveFollowerMessage message)
        {
            await _follows.DeleteOneAsync(f => f.Follower == message.FollowerId && f.Following == message.FollowingId);
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
            await _tweets.DeleteManyAsync(u => u.UserId == message.Id);
            await _follows.DeleteManyAsync(f => f.Follower == message.Id);
            await _follows.DeleteManyAsync(f => f.Following == message.Id);
        }

        public async Task DeleteTweet(string tweetId)
        {
            await _tweets.DeleteOneAsync(t => t.Id == tweetId);
        }
    }
}
