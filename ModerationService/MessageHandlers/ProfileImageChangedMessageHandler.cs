using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.MessageHandlers
{
    public class ProfileImageChangedMessageHandler : IMessageHandler<ProfileImageChangedMessage>
    {
        private readonly IModerationService _moderationService;

        public ProfileImageChangedMessageHandler(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        public async Task HandleMessageAsync(string messageType, ProfileImageChangedMessage message)
        {
            await _moderationService.UpdateUserImage(message);
        }
    }
}
