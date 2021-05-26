using FollowService.DAL.Config;
using FollowService.Entities;
using MongoDB.Driver;

namespace FollowService.DAL
{
    public class FollowContext : IFollowContext
    {
        private readonly IMongoDatabase _db;

        public FollowContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Follow> Follows => _db.GetCollection<Follow>("Follows");
    }
}
