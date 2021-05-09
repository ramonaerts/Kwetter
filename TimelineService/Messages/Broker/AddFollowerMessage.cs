using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimelineService.Messages.Broker
{
    public class AddFollowerMessage
    {
        public string Id { get; set; }
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }
    }
}
