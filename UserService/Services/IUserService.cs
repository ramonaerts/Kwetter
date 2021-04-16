using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Messages.API;

namespace UserService.Services
{
    public interface IUserService
    {
        bool RegisterUser(RegisterMessage message);
        bool VerifyPasswords(string password, string confirmPassword);
        bool VerifyUniqueEmail(string email);
    }
}
