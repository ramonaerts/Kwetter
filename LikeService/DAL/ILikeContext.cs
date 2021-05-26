using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Entities;
using MongoDB.Driver;

namespace LikeService.DAL
{
    public interface ILikeContext
    {
        IMongoCollection<UserLike> UserLikes { get; }
        IMongoCollection<TweetLike> TweetLikes { get; }
    }
}
