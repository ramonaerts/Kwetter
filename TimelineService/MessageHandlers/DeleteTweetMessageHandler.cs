using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class DeleteTweetMessageHandler : IMessageHandler<DeleteTweetMessage>
    {
        private readonly ITimelineService _timelineService;
        public DeleteTweetMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public async Task HandleMessageAsync(string messageType, DeleteTweetMessage message)
        {
            await _timelineService.DeleteTweet(message.Id);
        }
    }
}
