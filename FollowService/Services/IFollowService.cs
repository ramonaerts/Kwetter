using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FollowService.Messages.API;

namespace FollowService.Services
{
    public interface IFollowService
    {
        Task<bool> FollowExists(string followerId, string followingId);
        Task<bool> FollowUser(string followerId, string followingId);
        Task<bool> UnFollowUser(string followerId, string followingId);
    }
}
