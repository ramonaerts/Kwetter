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

            channel.QueueBind("TweetService", Exchange, "UserChange");
            channel.QueueBind("AuthenticationService", Exchange, "NewUserMessage");

            _configured = true;
        }
    }
}
