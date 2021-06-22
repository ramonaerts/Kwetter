using System;
using ModerationService.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace ModerationService.Entities
{
    public class Tweet
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TweetContent { get; set; }
        public string TweetDateTime { get; set; }
        public Status TweetStatus { get; set; }
    }
}
