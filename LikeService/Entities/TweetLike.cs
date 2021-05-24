using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace LikeService.Entities
{
    public class TweetLike
    {
        [BsonId]
        public string Id { get; set; }
        public string TweetId { get; set; }
        public int LikeCount { get; set; }
    }
}
