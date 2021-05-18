﻿using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Shared.Messaging
{
    internal class RabbitMqConfig
    {
        private readonly RabbitMqConnection _conn;
        private bool _configured;
        private const string Exchange = "kwetter";

        public RabbitMqConfig(RabbitMqConnection conn)
        {
            _conn = conn;
        }

        public void ConfigureRabbit()
        {
            if (_configured) return;

            var channel = _conn.CreateChannel();

            channel.ExchangeDeclare(Exchange, "fanout");

            channel.QueueDeclare("TweetService", false, false);
            channel.QueueDeclare("AuthenticationService", false, false);
            channel.QueueDeclare("UserService", false, false);
            channel.QueueDeclare("FileManagementService", false, false);
            channel.QueueDeclare("FollowService", false, false);
            channel.QueueDeclare("TimelineService", false, false);

            //QueueBind(Service to which the message will be send, Exchange, Message class names)
            channel.QueueBind("TweetService", Exchange, "UserChange");
            channel.QueueBind("TweetService", Exchange, "NewProfileMessage");
            channel.QueueBind("AuthenticationService", Exchange, "NewUserMessage");
            channel.QueueBind("FileManagementService", Exchange, "NewProfileImage");

            channel.QueueBind("AuthenticationService", Exchange, "EmailChangedMessage");
            channel.QueueBind("TweetService", Exchange, "ProfileChangedMessage");

            channel.QueueBind("UserService", Exchange, "ProfileImageChangedMessage");
            channel.QueueBind("TweetService", Exchange, "ProfileImageChangedMessage");

            channel.QueueBind("UserService", Exchange, "AddFollowerMessage");
            channel.QueueBind("UserService", Exchange, "RemoveFollowerMessage");

            channel.QueueBind("TimelineService", Exchange, "NewProfileMessage");
            channel.QueueBind("TimelineService", Exchange, "ProfileChangedMessage");
            channel.QueueBind("TimelineService", Exchange, "ProfileImageChangedMessage");
            channel.QueueBind("TimelineService", Exchange, "NewPostedTweet");
            channel.QueueBind("TimelineService", Exchange, "AddFollowerMessage");
            channel.QueueBind("TimelineService", Exchange, "RemoveFollowerMessage");

            channel.QueueBind("ModerationService", Exchange, "NewProfanityTweet");

            _configured = true;
        }
    }
}
