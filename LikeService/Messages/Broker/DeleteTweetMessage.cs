using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LikeService.Messages.Broker
{
    public class DeleteTweetMessage
    {
        public string TweetId { get; set; }
    }
}
