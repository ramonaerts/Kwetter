using System.Threading.Tasks;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Services;
using Shared.Messaging;

namespace AuthenticationService.MessageHandlers
{
    public class EmailChangedMessageHandler : IMessageHandler<EmailChangedMessage>
    {
        private readonly IAuthService _authService;

        public EmailChangedMessageHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task HandleMessageAsync(string messageType, EmailChangedMessage message)
        {
            await _authService.UpdateEmail(message);
        }
    }
}
