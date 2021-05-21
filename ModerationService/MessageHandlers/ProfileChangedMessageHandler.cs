using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.MessageHandlers
{
    public class ProfileChangedMessageHandler : IMessageHandler<ProfileChangedMessage>
    {
        private readonly IModerationService _moderationService;

        public ProfileChangedMessageHandler(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        public async Task HandleMessageAsync(string messageType, ProfileChangedMessage message)
        {
            await _moderationService.UpdateUser(message);
        }
    }
}
