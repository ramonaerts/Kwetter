﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages;
using TweetService.Services;

namespace TweetService.MessageHandlers
{
    public class TweetsMessageHandler : IMessageHandler<Tweets>
    {
        private readonly ITweetService _tweetService;
        public TweetsMessageHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, Tweets message)
        {
            foreach (var item in message.TweetList)
            {
                Console.WriteLine(item);
            }
        }
    }
}
