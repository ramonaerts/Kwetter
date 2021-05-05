using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Messages.Broker
{
    public class ProfileImageChangedMessage
    {
        public string Id { get; set; }
        public string Image { get; set; }
    }
}
