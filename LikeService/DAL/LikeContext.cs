using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.DAL;
using LikeService.DAL.Config;
using LikeService.Entities;
using MongoDB.Driver;

namespace LikeService.DAL
{
    public class LikeContext : ILikeContext
    {
        private readonly IMongoDatabase _db;

        public LikeContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<UserLike> UserLikes => _db.GetCollection<UserLike>("UserLikes");
        public IMongoCollection<TweetLike> TweetLikes => _db.GetCollection<TweetLike>("TweetLikes");
    }
}
