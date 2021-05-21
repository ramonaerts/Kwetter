﻿using System;

namespace ModerationService.Messages.Broker
{
    public class NewProfanityTweetMessage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
    }
}