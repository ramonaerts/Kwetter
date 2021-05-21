using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.MessageHandlers
{
    public class NewProfanityTweetMessageHandler : IMessageHandler<NewProfanityTweetMessage>
    {
        private readonly IModerationService _moderationService;

        public NewProfanityTweetMessageHandler(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        public async Task HandleMessageAsync(string messageType, NewProfanityTweetMessage message)
        {
            await _moderationService.AddProfanityTweet(message);
        }
    }
}
