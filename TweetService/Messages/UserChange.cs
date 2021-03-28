using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetService.Messages
{
    public class UserChange
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
    }
}
