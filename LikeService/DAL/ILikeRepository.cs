using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Entities;

namespace LikeService.DAL
{
    public interface ILikeRepository
    {
        UserLike GetUserLikeByUserId(string userId);
        TweetLike GetTweetLikeByTweetId(string tweetId);
        Task SaveUserLike(UserLike userLike);
        Task SaveTweetLike(TweetLike tweetLike);
        Task UpdateUserLike(UserLike userLike);
        Task UpdateTweetLike(TweetLike tweetLike);
        Task DeleteUserLike(string userId);
        Task DeleteTweetAndLikes(string tweetId);
    }
}
