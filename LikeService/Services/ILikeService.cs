using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeService.Services
{
    public interface ILikeService
    {
        Task<bool> NewLike(string userId, string tweetId);
    }
}
