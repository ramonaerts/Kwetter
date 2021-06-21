using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LikeService.Messages.Broker;
using LikeService.Services;
using Shared.Messaging;

namespace LikeService.MessageHandlers
{
    public class DeleteTweetMessageHandler : IMessageHandler<DeleteTweetMessage>
    {
        private readonly ILikeService _likeService;
        public DeleteTweetMessageHandler(ILikeService tweetService)
        {
            _likeService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, DeleteTweetMessage message)
        {
            await _likeService.DeleteTweet(message.Id);
        }
    }
}
