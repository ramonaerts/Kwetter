using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.MessageHandlers
{
    public class DeleteTweetMessageHandler : IMessageHandler<DeleteTweetMessage>
    {
        private readonly IModerationService _moderationService;
        public DeleteTweetMessageHandler(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        public async Task HandleMessageAsync(string messageType, DeleteTweetMessage message)
        {
            await _moderationService.DeleteTweet(message.TweetId);
        }
    }
}
