using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimelineService.Messages.Broker
{
    public class RemoveFollowerMessage
    {
        private string FollowId { get; set; }
        private string FollowingId { get; set; }
    }
}
