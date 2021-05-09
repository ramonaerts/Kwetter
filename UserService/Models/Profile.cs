using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Models
{
    public class Profile
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Image { get; set; }
        public bool Self { get; set; }
    }
}
