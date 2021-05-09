using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimelineService.Messages.Broker
{
    public class NewPostedTweetMessage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
    }
}
