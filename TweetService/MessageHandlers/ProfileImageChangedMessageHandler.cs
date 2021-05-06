﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages.Broker;
using TweetService.Services;

namespace TweetService.MessageHandlers
{
    public class ProfileImageChangedMessageHandler : IMessageHandler<ProfileImageChangedMessage>
    {
        private readonly ITweetService _tweetService;
        public ProfileImageChangedMessageHandler(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        public async Task HandleMessageAsync(string messageType, ProfileImageChangedMessage message)
        {
            _tweetService.UpdateUserImage(message);
        }
    }
}
