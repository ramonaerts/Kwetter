using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Services;
using Shared.Messaging;

namespace AuthenticationService.MessageHandlers
{
    public class UpgradeUserToAdminMessageHandler : IMessageHandler<UpgradeUserToAdminMessage>
    {
        private readonly IAuthService _authService;

        public UpgradeUserToAdminMessageHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task HandleMessageAsync(string messageType, UpgradeUserToAdminMessage message)
        {
            await _authService.UpgradeUserToAdmin(message);
        }
    }
}
