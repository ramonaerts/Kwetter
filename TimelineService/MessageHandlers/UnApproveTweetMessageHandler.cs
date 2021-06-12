using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class UnApproveTweetMessageHandler : IMessageHandler<UnApproveTweetMessage>
    {
        private readonly ITimelineService _timelineService;
        public UnApproveTweetMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public async Task HandleMessageAsync(string messageType, UnApproveTweetMessage message)
        {
            await _timelineService.UnApproveTweet(message);
        }
    }
}
