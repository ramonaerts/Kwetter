using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class RemoveFollowerMessageHandler : IMessageHandler<RemoveFollowerMessage>
    {
        private readonly ITimelineService _timelineService;

        public RemoveFollowerMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public async Task HandleMessageAsync(string messageType, RemoveFollowerMessage message)
        {
            await _timelineService.UnFollowUser(message);
        }
    }
}
