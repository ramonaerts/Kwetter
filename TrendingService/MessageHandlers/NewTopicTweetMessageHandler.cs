using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TrendingService.Messages.Broker;
using TrendingService.Services;

namespace TrendingService.MessageHandlers
{
    public class NewTopicTweetMessageHandler : IMessageHandler<NewTopicTweetMessage>
    {
        private readonly ITrendingService _trendingService;

        public NewTopicTweetMessageHandler(ITrendingService trendingService)
        {
            _trendingService = trendingService;
        }

        public async Task HandleMessageAsync(string messageType, NewTopicTweetMessage message)
        {
            await _trendingService.AddNewTopic(message);
        }
    }
}
