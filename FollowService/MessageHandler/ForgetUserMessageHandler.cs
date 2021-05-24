using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FollowService.Messages.Broker;
using FollowService.Services;
using Shared.Messaging;

namespace FollowService.MessageHandler
{
    public class ForgetUserMessageHandler : IMessageHandler<ForgetUserMessage>
    {
        private readonly IFollowService _followService;

        public ForgetUserMessageHandler(IFollowService followService)
        {
            _followService = followService;
        }

        public async Task HandleMessageAsync(string messageType, ForgetUserMessage message)
        {
            await _followService.ForgetUser(message);
        }
    }
}
