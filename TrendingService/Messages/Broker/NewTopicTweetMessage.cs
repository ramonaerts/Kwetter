using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrendingService.Messages.Broker
{
    public class NewTopicTweetMessage
    {
        public string Id { get; set; }
        public string TweetContent { get; set; }
    }
}
