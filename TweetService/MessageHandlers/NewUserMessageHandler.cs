using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages;
using TweetService.Services;

namespace TweetService.MessageHandlers
{
    public class NewUserMessageHandler : IMessageHandler<User>
    {
        private readonly ITweetService _tweetService;
        public NewUserMessageHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, User message)
        {
            _tweetService.AddUser(message);
        }
    }
}
