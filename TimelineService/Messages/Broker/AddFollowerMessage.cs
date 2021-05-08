using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Services;

namespace TimelineService.Messages.Broker
{
    public class AddFollowerMessage
    {
        private string FollowId { get; set; }
        private string FollowingId { get; set; }
    }
}
