using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FollowService.DAL;
using FollowService.Entities;
using FollowService.Messages.API;
using MongoDB.Driver;

namespace FollowService.Services
{
    public class FollowService : IFollowService
    {
        private readonly IMongoCollection<Follow> _follows;

        public FollowService(IFollowContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _follows = database.GetCollection<Follow>("Follows");
        }

        public async Task<bool> FollowExists(string followerId, string followingId)
        {
            var follow = await _follows.Find(f => f.Follower == followerId && f.Following == followingId).FirstOrDefaultAsync();

            return follow.Id != null;
        }

        public async Task<bool> FollowUser(string followerId, string followingId)
        {
            if (await FollowExists(followerId, followingId)) return false;

            var follow = new Follow
            {
                Id = Guid.NewGuid().ToString(),
                Follower = followerId,
                Following = followingId
            };

            await _follows.InsertOneAsync(follow);

            return true;
        }

        public async Task<bool> UnFollowUser(string followerId, string followingId)
        {
            if (!await FollowExists(followerId, followingId)) return false;

            await _follows.DeleteOneAsync(f => f.Follower == followerId && f.Following == followingId);

            return true;
        }
    }
}
