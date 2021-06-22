using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Entities;
using LikeService.Messages.Broker;
using LikeService.Models;

namespace LikeService.Services
{
    public interface ILikeService
    {
        Task<bool> NewLike(string userId, string tweetId);
        Task<bool> RemoveLike(string userId, string tweetId);
        Like GetLikes(string userId, string tweetId);
        Task ForgetUser(ForgetUserMessage message);
        Task DeleteTweet(string tweetId);
    }
}
