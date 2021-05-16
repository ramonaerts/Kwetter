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

        public async Task HandleMessageAsync(string messageType, AddFollowerMessage message)
        {
            await _timelineService.FollowUser(message);
        }
    }
}
