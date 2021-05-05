using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Messages.API
{
    public class EditProfileMessage
    {
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
}
