using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class ProfileImageChangedMessageHandler : IMessageHandler<ProfileImageChangedMessage>
    {
        private readonly ITimelineService _timelineService;

        public ProfileImageChangedMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public async Task HandleMessageAsync(string messageType, ProfileImageChangedMessage message)
        {
            await _timelineService.UpdateUserImage(message);
        }
    }
}
