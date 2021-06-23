using System;
using MongoDB.Bson.Serialization.Attributes;

namespace TweetService.Entities
{
    public class Tweet
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TweetContent { get; set; }
        public string TweetDateTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
