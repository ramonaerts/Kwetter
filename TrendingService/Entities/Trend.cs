using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace TrendingService.Entities
{
    public class Trend
    {
        [BsonId]
        public int Id { get; set; }
        public string Topic { get; set; }
        public int TweetCount { get; set; }
        public List<string> TweetIds { get; set; }
    }
}
