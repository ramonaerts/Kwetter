using RabbitMQ.Client;
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

        public RabbitMqConfig(RabbitMqConnection conn)
        {
            _conn = conn;
        }

        public void ConfigureRabbit()
        {
            if (_configured) return;

            var channel = _conn.CreateChannel();

            channel.ExchangeDeclare("kwetter", "fanout");

            channel.QueueDeclare("TweetService", false, false);
            channel.QueueDeclare("AuthenticationService", false, false);

            channel.QueueBind("TweetService", "kwetter", "UserChange");

            _configured = true;
        }
    }
}
