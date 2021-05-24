using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.DAL;
using LikeService.Entities;
using MongoDB.Driver;

namespace LikeService.Services
{
    public class LikeService : ILikeService
    {
        private readonly IMongoCollection<Like> _likes;
        private readonly IMongoCollection<UserLike> _userLikes;

        public LikeService(ILikeContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _likes = database.GetCollection<Like>("Likes");
            _userLikes = database.GetCollection<UserLike>("UserLikes");
        }

        public async Task<bool> NewLike(string userId, string tweetId)
        {
            throw new NotImplementedException();
        }
    }
}
