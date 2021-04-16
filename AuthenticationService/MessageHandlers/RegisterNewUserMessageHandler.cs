using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Services;
using Shared.Messaging;

namespace AuthenticationService.MessageHandlers
{
    public class RegisterNewUserMessageHandler : IMessageHandler<NewUserMessage>
    {
        private readonly IAuthService _authService;

        public RegisterNewUserMessageHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task HandleMessageAsync(string messageType, NewUserMessage message)
        {
            _authService.AddUser(message);
        }
    }
}
