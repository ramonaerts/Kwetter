using System;
using System.Threading.Tasks;
using FollowService.DAL;
using FollowService.Entities;
using MongoDB.Driver;
using Shared.Messaging;

namespace FollowService.Services
{
    public class FollowService : IFollowService
    {
        private readonly IMongoCollection<Follow> _follows;
        private readonly IMessagePublisher _messagePublisher;

        public FollowService(IFollowContext context, IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;

            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _follows = database.GetCollection<Follow>("Followings");
        }

        public async Task<bool> FollowExists(string followerId, string followingId)
        {
            var follow = await _follows.Find(f => f.Follower == followerId && f.Following == followingId).FirstOrDefaultAsync();

            return follow != null;
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

            await _messagePublisher.PublishMessageAsync("AddFollowerMessage", new { Id = follow.Id, FollowerId = follow.Follower, FollowingId = follow.Following });

            await _follows.InsertOneAsync(follow);

            return true;
        }

        public async Task<bool> UnFollowUser(string followerId, string followingId)
        {
            if (!await FollowExists(followerId, followingId)) return false;

            await _messagePublisher.PublishMessageAsync("RemoveFollowerMessage", new { FollowerId = followerId, FollowingId = followingId });

            await _follows.DeleteOneAsync(f => f.Follower == followerId && f.Following == followingId);

            return true;
        }
    }
}
