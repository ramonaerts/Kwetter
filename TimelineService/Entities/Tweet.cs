using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace TimelineService.Entities
{
    public class Tweet
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
    }
}
