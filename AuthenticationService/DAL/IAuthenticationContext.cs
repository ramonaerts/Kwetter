using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationService.Entities;

namespace AuthenticationService.DAL
{
    public interface IAuthenticationContext
    {
        User LoginUser(string email, string password);
    }
}
