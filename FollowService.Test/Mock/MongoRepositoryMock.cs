using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FollowService.DAL;
using FollowService.Entities;

namespace FollowService.Test.Mock
{
    public class MongoRepositoryMock : IFollowRepository
    {
        public List<Follow> Follows;

        public MongoRepositoryMock()
        {
            Follows = new List<Follow>();
        }

        public Follow GetFollow(string followerId, string followingId)
        {
            return Follows.Find(f => f.Follower == followerId && f.Following == followingId);
        }

        public Task AddOneFollower(Follow follow)
        {
            Follows.Add(follow);
            return Task.CompletedTask;
        }

        public Task DeleteOneFollower(string followerId, string followingId)
        {
            Follows.RemoveAll(f => f.Follower == followerId && f.Following == followingId);
            return Task.CompletedTask;
        }

        public Task DeleteAllFollowers(string userId)
        {
            Follows.RemoveAll(f => f.Follower == userId);
            return Task.CompletedTask;
        }

        public Task DeleteAllFollowing(string userId)
        {
            Follows.RemoveAll(f => f.Following == userId);
            return Task.CompletedTask;
        }

        public int GetAllFollowers(string userId)
        {
            return Follows.Count(f => f.Following == userId);
        }

        public int GetAllFollowings(string userId)
        {
            return Follows.Count(f => f.Follower == userId);
        }
    }
}
