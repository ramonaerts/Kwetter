using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class NewPostedTweetMessageHandler : IMessageHandler<NewPostedTweetMessage>
    {
        private readonly ITimelineService _timelineService;

        public NewPostedTweetMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public Task HandleMessageAsync(string messageType, NewPostedTweetMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
