using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class ForgetUserMessageHandler : IMessageHandler<ForgetUserMessage>
    {
        private readonly ITimelineService _timelineService;

        public ForgetUserMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public async Task HandleMessageAsync(string messageType, ForgetUserMessage message)
        {
            await _timelineService.ForgetUser(message);
        }
    }
}
