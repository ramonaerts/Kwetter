using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Messaging;
using TweetService.Messages;

namespace TweetService.MessageHandlers
{
    public class TweetsMessageHandler : IMessageHandler<Tweets>
    {
        public TweetsMessageHandler()
        {
            
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
