using System.Threading.Tasks;
using UserService.Messages.API;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(RegisterMessage message);
        Task<bool> CreateAdmin(RegisterMessage message);
        bool VerifyPasswords(string password, string confirmPassword);
        bool VerifyUniqueEmail(string email);
        Task<bool> ForgetUser(string id);
    }
}
