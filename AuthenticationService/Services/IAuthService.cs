using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Entities;
using AuthenticationService.Messages.Api;
using AuthenticationService.Messages.Broker;

namespace AuthenticationService.Services
{
    public interface IAuthService
    {
        User LoginUser(LoginMessage message);
        void AddUser(NewUserMessage message);
        string CreateToken(string userId);
    }
}
