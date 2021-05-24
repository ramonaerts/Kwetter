using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Messages.Broker;
using LikeService.Services;
using Shared.Messaging;

namespace LikeService.MessageHandlers
{
    public class ForgetUserMessageHandler : IMessageHandler<ForgetUserMessage>
    {
        private readonly ILikeService _likeService;

        public ForgetUserMessageHandler(ILikeService likeService)
        {
            _likeService = likeService;
        }

        public async Task HandleMessageAsync(string messageType, ForgetUserMessage message)
        {
            await _likeService.ForgetUser(message);
        }
    }
}
