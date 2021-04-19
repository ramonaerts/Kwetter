using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages;
using TweetService.Messages.Broker;
using TweetService.Services;

namespace TweetService.MessageHandlers
{
    public class NewUserMessageHandler : IMessageHandler<NewProfileMessage>
    {
        private readonly ITweetService _tweetService;
        public NewUserMessageHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, NewProfileMessage message)
        {
            _tweetService.AddUser(message);
        }
    }
}
