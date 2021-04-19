using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetService.Messages.Broker
{
    public class NewProfileMessage
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Image { get; set; }
    }
}
