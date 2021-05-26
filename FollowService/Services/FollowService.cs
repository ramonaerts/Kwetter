using System;
using System.Threading.Tasks;
using FollowService.DAL;
using FollowService.Entities;
using FollowService.Messages.Broker;
using MongoDB.Driver;
using Shared.Messaging;

namespace FollowService.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IMessagePublisher _messagePublisher;

        public FollowService(IMessagePublisher messagePublisher, IFollowRepository followRepository)
        {
            _messagePublisher = messagePublisher;
            _followRepository = followRepository;
        }

        public bool FollowExists(string followerId, string followingId)
        {
            var follow = _followRepository.GetFollow(followerId, followingId);

            return follow != null;
        }

        public async Task<bool> FollowUser(string followerId, string followingId)
        {
            if (FollowExists(followerId, followingId)) return false;

            var follow = new Follow
            {
                Id = Guid.NewGuid().ToString(),
                Follower = followerId,
                Following = followingId
            };

            await _messagePublisher.PublishMessageAsync("AddFollowerMessage", new { Id = follow.Id, FollowerId = follow.Follower, FollowingId = follow.Following });

            await _followRepository.AddOneFollower(follow);

            return true;
        }

        public async Task<bool> UnFollowUser(string followerId, string followingId)
        {
            if (!FollowExists(followerId, followingId)) return false;

            await _messagePublisher.PublishMessageAsync("RemoveFollowerMessage", new { FollowerId = followerId, FollowingId = followingId });

            await _followRepository.DeleteOneFollower(followerId, followingId);

            return true;
        }

        public async Task ForgetUser(ForgetUserMessage message)
        {
            await _followRepository.DeleteAllFollowers(message.Id);
            await _followRepository.DeleteAllFollowing(message.Id);
        }
    }
}
