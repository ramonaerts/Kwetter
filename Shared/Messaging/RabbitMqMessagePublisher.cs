using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client.Exceptions;

namespace Shared.Messaging
{
    internal class RabbitMqMessagePublisher : IMessagePublisher
    {
        private readonly string _queueName;
        private readonly RabbitMqConnection _connection;

        public RabbitMqMessagePublisher(QueueName queueName, RabbitMqConnection connection)
        {
            _queueName = queueName.Name;
            _connection = connection;
        }

        public Task PublishMessageAsync<T>(string messageType, T value)
        {
            try
            {
                SendMessageAsync(messageType, value);
            }
            catch (AlreadyClosedException)
            {
                QueuedTasks.QueueMethod(() => SendMessageAsync(messageType, value));
            }

            return Task.CompletedTask;
        }

        public Task SendMessageAsync<T>(string messageType, T value)
        {
            using var channel = _connection.CreateChannel();
            var message = channel.CreateBasicProperties();
            message.ContentType = "application/json";
            message.DeliveryMode = 2;
            // Add a MessageType header, this part is crucial for our solution because it is our way of distinguishing messages
            message.Headers = new Dictionary<string, object> { ["MessageType"] = messageType };
            var body = JsonSerializer.SerializeToUtf8Bytes(value);

            // Publish this without a routing key to the rabbitmq broker
            channel.BasicPublish("kwetter", string.Empty, message, body);
            return Task.CompletedTask;
        }
    }
}
