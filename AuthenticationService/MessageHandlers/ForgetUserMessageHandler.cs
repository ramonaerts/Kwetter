using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Services;
using Shared.Messaging;

namespace AuthenticationService.MessageHandlers
{
    public class ForgetUserMessageHandler : IMessageHandler<ForgetUserMessage>
    {
        private readonly IAuthService _authService;

        public ForgetUserMessageHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task HandleMessageAsync(string messageType, ForgetUserMessage message)
        {
            await _authService.ForgetUser(message);
        }
    }
}
