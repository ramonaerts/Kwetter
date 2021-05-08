using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class AddFollowerMessageHandler : IMessageHandler<AddFollowerMessage>
    {
        private readonly ITimelineService _timelineService;

        public AddFollowerMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public Task HandleMessageAsync(string messageType, AddFollowerMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
