using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimelineService.Messages.Broker
{
    public class RemoveFollowerMessage
    {
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }
    }
}
