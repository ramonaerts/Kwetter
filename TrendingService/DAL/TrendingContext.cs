using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TrendingService.DAL.Config;
using TrendingService.Entities;

namespace TrendingService.DAL
{
    public class TrendingContext : ITrendingContext
    {
        private readonly IMongoDatabase _db;

        public TrendingContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<Trend> Trends => _db.GetCollection<Trend>("Trends");
    }
}
