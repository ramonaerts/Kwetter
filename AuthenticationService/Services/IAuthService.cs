using System.Threading.Tasks;
using AuthenticationService.Entities;
using AuthenticationService.Messages.Api;
using AuthenticationService.Messages.Broker;
using AuthenticationService.Models;

namespace AuthenticationService.Services
{
    public interface IAuthService
    {
        User LoginUser(LoginMessage message);
        Task AddUser(NewUserMessage message);
        Task UpdateEmail(EmailChangedMessage message);
        string CreateToken(string userId, UserRole role);
        Task ForgetUser(ForgetUserMessage message);
    }
}
