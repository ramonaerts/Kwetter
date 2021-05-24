using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Services;
using Shared.Messaging;

namespace ModerationService.MessageHandlers
{
    public class ForgetUserMessageHandler : IMessageHandler<ForgetUserMessage>
    {
        private readonly IModerationService _moderationService;

        public ForgetUserMessageHandler(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        public async Task HandleMessageAsync(string messageType, ForgetUserMessage message)
        {
            await _moderationService.ForgetUser(message);
        }
    }
}
