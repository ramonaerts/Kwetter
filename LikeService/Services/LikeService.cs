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
        private readonly ILikeRepository _likeRepository;

        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }

        public async Task<bool> NewLike(string userId, string tweetId)
        {
            var userLike = _likeRepository.GetUserLikeByUserId(userId);
            if (userLike == null)
            {
                userLike = CreateNewUserLike(userId);

                await _likeRepository.SaveUserLike(userLike);
            }

            var tweetLike = _likeRepository.GetTweetLikeByTweetId(tweetId);
            if (tweetLike == null)
            {
                tweetLike = CreateNewTweetLike(tweetId);

                await _likeRepository.SaveTweetLike(tweetLike);
            }

            if (CheckIfUserLikesTweet(tweetId, userId)) return false;

            tweetLike.LikeCount += 1;
            userLike.UserLikes.Add(tweetId);

            await _likeRepository.UpdateTweetLike(tweetLike);
            await _likeRepository.UpdateUserLike(userLike);

            return true;
        }

        public async Task<bool> RemoveLike(string userId, string tweetId)
        {
            if (!CheckIfUserLikesTweet(tweetId, userId)) return false;

            var tweetLike = _likeRepository.GetTweetLikeByTweetId(tweetId);
            tweetLike.LikeCount -= 1;
            await _likeRepository.UpdateTweetLike(tweetLike);

            var userLike = _likeRepository.GetUserLikeByUserId(userId);
            userLike.UserLikes.Remove(tweetId);
            await _likeRepository.UpdateUserLike(userLike);

            return true;
        }

        public Like GetLikes(string userId, string tweetId)
        {
            var tweetLike = _likeRepository.GetTweetLikeByTweetId(tweetId);
            var likeCount = 0;
            if (tweetLike != null) likeCount = tweetLike.LikeCount;

            var userLike = _likeRepository.GetUserLikeByUserId(userId);
            var liked = userLike != null && userLike.UserLikes.Contains(tweetId);

            return new Like
            {
                LikeCount = likeCount,
                Liked = liked
            };
        }

        private TweetLike CreateNewTweetLike(string tweetId)
        {
            return new TweetLike
            {
                Id = Guid.NewGuid().ToString(),
                LikeCount = 0,
                TweetId = tweetId
            };
        }

        private UserLike CreateNewUserLike(string userId)
        {
            return new UserLike
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                UserLikes = new List<string>()
            };
        }

        private bool CheckIfUserLikesTweet(string tweetId, string userId)
        {
            var userLike = _likeRepository.GetUserLikeByUserId(userId);

            return userLike != null && userLike.UserLikes.Contains(tweetId);
        }

        public async Task ForgetUser(ForgetUserMessage message)
        {
            await _likeRepository.DeleteUserLike(message.Id);
        }
    }
}
