using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetService.Messages.Broker
{
    public class UnApproveTweetMessage
    {
        public string TweetId { get; set; }
    }
}
