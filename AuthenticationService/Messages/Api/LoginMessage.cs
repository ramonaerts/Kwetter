using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Messages.Api
{
    public class LoginMessage
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
