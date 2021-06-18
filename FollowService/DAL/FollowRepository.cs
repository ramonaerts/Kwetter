using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FollowService.Entities;
using MongoDB.Driver;

namespace FollowService.DAL
{
    public class FollowRepository : IFollowRepository
    {
        private readonly IFollowContext _context;

        public FollowRepository(IFollowContext context)
        {
            _context = context;
        }

        public Follow GetFollow(string followerId, string followingId)
        {
            var follows = _context.Follows.AsQueryable().ToList();

            return follows.FirstOrDefault(f => f.Follower == followerId && f.Following == followingId);
        }

        public async Task AddOneFollower(Follow follow)
        {
            await _context.Follows.InsertOneAsync(follow);
        }

        public async Task DeleteOneFollower(string followerId, string followingId)
        {
            await _context.Follows.DeleteOneAsync(f => f.Follower == followerId && f.Following == followingId);
        }

        public async Task DeleteAllFollowers(string userId)
        {
            await _context.Follows.DeleteManyAsync(f => f.Follower == userId);
        }

        public async Task DeleteAllFollowing(string userId)
        {
            await _context.Follows.DeleteManyAsync(f => f.Following == userId);
        }

        public int GetAllFollowers(string userId)
        {
            var follows = _context.Follows.AsQueryable().ToList();

            return follows.Count(f => f.Following == userId);
        }

        public int GetAllFollowings(string userId)
        {
            var follows = _context.Follows.AsQueryable().ToList();

            return follows.Count(f => f.Follower == userId);
        }
    }
}
