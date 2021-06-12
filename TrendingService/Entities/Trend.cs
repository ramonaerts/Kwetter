using System.Collections.Generic;

namespace TrendingService.Entities
{
    public class Trend
    {
        public string Topic { get; set; }
        public int TweetCount { get; set; }
        public List<string> TweetIds { get; set; }
    }
}
