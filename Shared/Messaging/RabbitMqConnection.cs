using System;
using RabbitMQ.Client;

namespace Shared.Messaging
{
    /// <summary>
    /// Singleton service keeping track of our connection with RabbitMQ.
    /// </summary>
    internal class RabbitMqConnection : IDisposable
    {
        private IConnection _connection;
        public IModel CreateChannel()
        {
            var connection = GetConnection();
            return connection.CreateModel();
        }

        private IConnection GetConnection()
        {
            if (_connection == null)
            {
                var factory = new ConnectionFactory
                {
                    HostName = "rabbit",
                    UserName = "guest",
                    Password = "guest",
                    //Uri = new Uri("amqp://guest:guest@localhost:15672/"),
                    AutomaticRecoveryEnabled = true // When the connection is lost, this will automatically reconnect us when it can get back up
                };
                _connection = factory.CreateConnection();
            }

            return _connection;
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
