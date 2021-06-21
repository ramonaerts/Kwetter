using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Messages.Broker
{
    public class NewAdminMessage
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
