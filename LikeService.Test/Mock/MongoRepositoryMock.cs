using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LikeService.DAL;
using LikeService.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LikeService.Test.Mock
{
    public class MongoRepositoryMock : ILikeRepository
    {
        public List<UserLike> UserLikes;
        public List<TweetLike> TweetLikes;

        public MongoRepositoryMock()
        {
            UserLikes = new List<UserLike>();
            TweetLikes = new List<TweetLike>();
        }

        public UserLike GetUserLikeByUserId(string userId)
        {
            return UserLikes.Find(u => u.UserId == userId);
        }

        public TweetLike GetTweetLikeByTweetId(string tweetId)
        {
            return TweetLikes.Find(u => u.TweetId == tweetId);
        }

        public Task SaveUserLike(UserLike userLike)
        {
            UserLikes.Add(userLike);
            return Task.CompletedTask;
        }

        public Task SaveTweetLike(TweetLike tweetLike)
        {
            TweetLikes.Add(tweetLike);
            return Task.CompletedTask;
        }

        public Task UpdateUserLike(UserLike userLike)
        {
            UserLikes[UserLikes.FindIndex(u => u.Id.Equals(userLike.Id))] = userLike;
            return Task.CompletedTask;
        }

        public Task UpdateTweetLike(TweetLike tweetLike)
        {
            TweetLikes[TweetLikes.FindIndex(t => t.Id.Equals(tweetLike.Id))] = tweetLike;
            return Task.CompletedTask;
        }

        public Task DeleteUserLike(string userId)
        {
            UserLikes.RemoveAll(u => u.Id == userId);
            return Task.CompletedTask;
        }

        public Task DeleteTweetAndLikes(string tweetId)
        {
            throw new NotImplementedException();
        }
    }
}
