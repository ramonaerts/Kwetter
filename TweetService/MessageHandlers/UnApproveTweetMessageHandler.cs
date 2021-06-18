using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages.Broker;
using TweetService.Services;

namespace TweetService.MessageHandlers
{
    public class UnApproveTweetMessageHandler : IMessageHandler<UnApproveTweetMessage>
    {
        private readonly ITweetService _tweetService;
        public UnApproveTweetMessageHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, UnApproveTweetMessage message)
        {
            await _tweetService.UnApproveTweet(message);
        }
    }
}
