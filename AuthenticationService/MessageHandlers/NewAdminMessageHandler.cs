using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Services;
using Shared.Messaging;

namespace AuthenticationService.MessageHandlers
{
    public class NewAdminMessageHandler : IMessageHandler<NewAdminMessage>
    {
        private readonly IAuthService _authService;

        public NewAdminMessageHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task HandleMessageAsync(string messageType, NewAdminMessage message)
        {
            await _authService.AddModerator(message);
        }
    }
}
