using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Messages.API;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
            
        }

        public bool RegisterUser(RegisterMessage message)
        {
            return false;
        }

        public bool VerifyPasswords(string password, string confirmPassword)
        {
            return false;
        }

        public bool VerifyUniqueEmail(string email)
        {
            return false;
        }
    }
}
