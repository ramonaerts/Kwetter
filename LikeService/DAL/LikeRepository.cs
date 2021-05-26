using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Entities;
using MongoDB.Driver;

namespace LikeService.DAL
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ILikeContext _context;

        public LikeRepository(ILikeContext context)
        {
            _context = context;
        }

        public UserLike GetUserLikeByUserId(string userId)
        {
            var userLikes = _context.UserLikes.AsQueryable().ToList();

            return userLikes.FirstOrDefault(u => u.UserId == userId);
        }

        public TweetLike GetTweetLikeByTweetId(string tweetId)
        {
            var tweetLikes = _context.TweetLikes.AsQueryable().ToList();

            return tweetLikes.FirstOrDefault(u => u.TweetId == tweetId);
        }

        public async Task SaveUserLike(UserLike userLike)
        {
            await _context.UserLikes.InsertOneAsync(userLike);
        }

        public async Task SaveTweetLike(TweetLike tweetLike)
        {
            await _context.TweetLikes.InsertOneAsync(tweetLike);
        }

        public async Task UpdateUserLike(UserLike userLike)
        {
            await _context.UserLikes.ReplaceOneAsync(t => t.Id == userLike.Id, userLike);
        }

        public async Task UpdateTweetLike(TweetLike tweetLike)
        {
            await _context.TweetLikes.ReplaceOneAsync(t => t.Id == tweetLike.Id, tweetLike);
        }

        public async Task DeleteUserLike(string userId)
        {
            await _context.UserLikes.DeleteOneAsync(u => u.UserId == userId);
        }
    }
}
