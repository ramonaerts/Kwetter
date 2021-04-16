using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.DAL;
using UserService.Entities;
using UserService.Messages.API;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public bool RegisterUser(RegisterMessage message)
        {
            var user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Email = message.Email,
                Username = message.Username,
                Nickname = message.Username,
                Image = "default.png"
            };

            _userContext.Add(user);
            _userContext.SaveChanges();

            return true;
        }

        public bool VerifyPasswords(string password, string confirmPassword)
        {
            return password == confirmPassword;
        }

        public bool VerifyUniqueEmail(string email)
        {
            return _userContext.Users.Any(e => e.Email == email);
        }
    }
}
