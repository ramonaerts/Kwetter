using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationService.Messages.Broker
{
    public class EmailChangedMessage
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
