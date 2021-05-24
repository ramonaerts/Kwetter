using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.DAL;
using LikeService.Entities;
using LikeService.Messages.Broker;
using LikeService.Models;
using MongoDB.Driver;

namespace LikeService.Services
{
    public class LikeService : ILikeService
    {
        private readonly IMongoCollection<TweetLike> _tweetLikes;
        private readonly IMongoCollection<UserLike> _userLikes;

        public LikeService(ILikeContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweetLikes = database.GetCollection<TweetLike>("TweetLikes");
            _userLikes = database.GetCollection<UserLike>("UserLikes");
        }

        public async Task<bool> NewLike(string userId, string tweetId)
        {
            var userLike = _userLikes.Find(u => u.UserId == userId).FirstOrDefault();
            if (userLike == null)
            {
                userLike = CreateNewUserLike(userId);

                await _userLikes.InsertOneAsync(userLike);
            }

            var tweetLike = _tweetLikes.Find(t => t.TweetId == tweetId).FirstOrDefault();
            if (tweetLike == null)
            {
                tweetLike = CreateNewTweetLike(tweetId);

                await _tweetLikes.InsertOneAsync(tweetLike);
            }

            if (CheckIfUserLikesTweet(tweetId, userId)) return false;

            tweetLike.LikeCount += 1;
            userLike.UserLikes.Add(tweetId);

            await _tweetLikes.ReplaceOneAsync(t => t.Id == tweetLike.Id, tweetLike);
            await _userLikes.ReplaceOneAsync(u => u.Id == userLike.Id, userLike);

            return true;
        }

        public async Task<bool> RemoveLike(string userId, string tweetId)
        {
            if (!CheckIfUserLikesTweet(tweetId, userId)) return false;

            var tweetLike = _tweetLikes.Find(t => t.TweetId == tweetId).FirstOrDefault();
            tweetLike.LikeCount -= 1;
            await _tweetLikes.ReplaceOneAsync(t => t.Id == tweetLike.Id, tweetLike);

            var userLike = _userLikes.Find(u => u.UserId == userId).FirstOrDefault();
            userLike.UserLikes.Remove(tweetId);
            await _userLikes.ReplaceOneAsync(u => u.Id == userLike.Id, userLike);

            return true;
        }

        public Like GetLikes(string userId, string tweetId)
        {
            var tweetLike = _tweetLikes.Find(t => t.TweetId == tweetId).FirstOrDefault();
            var userLike = _userLikes.Find(u => u.UserId == userId).FirstOrDefault();

            return new Like
            {
                LikeCount = tweetLike.LikeCount,
                Liked = userLike.UserLikes.Contains(tweetId)
            };
        }

        public TweetLike CreateNewTweetLike(string tweetId)
        {
            return new TweetLike
            {
                Id = Guid.NewGuid().ToString(),
                LikeCount = 0,
                TweetId = tweetId
            };
        }

        public UserLike CreateNewUserLike(string userId)
        {
            return new UserLike
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                UserLikes = new List<string>()
            };
        }

        public bool CheckIfUserLikesTweet(string tweetId, string userId)
        {
            var userLike = _userLikes.Find(u => u.UserId == userId).FirstOrDefault();

            return userLike.UserLikes.Contains(tweetId);
        }

        public async Task ForgetUser(ForgetUserMessage message)
        {
            await _userLikes.DeleteOneAsync(u => u.UserId == message.Id);
        }
    }
}
