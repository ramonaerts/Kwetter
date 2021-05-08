using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TimelineService.DAL;
using TimelineService.Entities;

namespace TimelineService.Services
{
    public class TimelineService : ITimelineService
    {
        private readonly IMongoCollection<Tweet> _tweets;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Follow> _follows;

        public TimelineService(ITimelineContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Entities.Tweet>("Tweets");
            _users = database.GetCollection<Entities.User>("Users");
            _follows = database.GetCollection<Follow>("Followings");
        }

        public Task GetUserTimeline(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
