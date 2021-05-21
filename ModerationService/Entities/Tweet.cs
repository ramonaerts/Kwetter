using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ModerationService.Entities
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
