using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Messages.Broker;
using LikeService.Services;
using Shared.Messaging;

namespace LikeService.MessageHandlers
{
    public class UnApproveTweetMessageHandler : IMessageHandler<UnApproveTweetMessage>
    {
        private readonly ILikeService _likeService;
        public UnApproveTweetMessageHandler(ILikeService tweetService)
        {
            _likeService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, UnApproveTweetMessage message)
        {
            await _likeService.UnApproveTweet(message);
        }
    }
}
