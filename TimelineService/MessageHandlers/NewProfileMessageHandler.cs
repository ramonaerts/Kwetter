using System.Threading.Tasks;
using Shared.Messaging;
using TimelineService.Messages.Broker;
using TimelineService.Services;

namespace TimelineService.MessageHandlers
{
    public class NewProfileMessageHandler : IMessageHandler<NewProfileMessage>
    {
        private readonly ITimelineService _timelineService;

        public NewProfileMessageHandler(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        public async Task HandleMessageAsync(string messageType, NewProfileMessage message)
        {
            await _timelineService.AddUser(message);
        }
    }
}