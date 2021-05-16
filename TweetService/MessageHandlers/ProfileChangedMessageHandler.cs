using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages.Broker;
using TweetService.Services;

namespace TweetService.MessageHandlers
{
    public class ProfileChangedMessageHandler : IMessageHandler<ProfileChangedMessage>
    {
        private readonly ITweetService _tweetService;
        public ProfileChangedMessageHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, ProfileChangedMessage message)
        {
            await _tweetService.UpdateUser(message);
        }
    }
}
