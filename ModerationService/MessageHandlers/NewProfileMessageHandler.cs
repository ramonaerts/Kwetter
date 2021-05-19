using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.MessageHandlers
{
    public class NewProfileMessageHandler : IMessageHandler<NewProfileMessage>
    {
        private readonly IModerationService _moderationService;

        public NewProfileMessageHandler(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        public async Task HandleMessageAsync(string messageType, NewProfileMessage message)
        {
            await _moderationService.AddUser(message);
        }
    }
}