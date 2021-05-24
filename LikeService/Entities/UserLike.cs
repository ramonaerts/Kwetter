using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace LikeService.Entities
{
    public class UserLike
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<string> UserLikes { get; set; }
    }
}
