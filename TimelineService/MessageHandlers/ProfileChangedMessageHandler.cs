using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class ProfileChangedMessageHandler : IMessageHandler<ProfileChangedMessage>
    {
        private readonly ITimelineService _timelineService;

        public ProfileChangedMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public Task HandleMessageAsync(string messageType, ProfileChangedMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
