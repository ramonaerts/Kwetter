using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Models;

namespace LikeService.Services
{
    public interface ILikeService
    {
        Task<bool> NewLike(string userId, string tweetId);
        Task<bool> RemoveLike(string userId, string tweetId);
        Like GetLikes(string userId, string tweetId);
        bool CheckIfUserLikesTweet(string tweetId, string userId);
    }
}
