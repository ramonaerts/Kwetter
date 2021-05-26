using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shared.Messaging;

namespace LikeService.Test.Mock
{
    public class MessagePublisherMock : IMessagePublisher
    {
        public Task PublishMessageAsync<T>(string messageType, T value)
        {
            return Task.CompletedTask;
        }
    }
}
