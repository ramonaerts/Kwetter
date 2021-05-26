using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FollowService.Entities;

namespace FollowService.DAL
{
    public interface IFollowRepository
    {
        Follow GetFollow(string followerId, string followingId);
        Task AddOneFollower(Follow follow);
        Task DeleteOneFollower(string followerId, string followingId);
        Task DeleteAllFollowers(string userId);
        Task DeleteAllFollowing(string userId);
    }
}
