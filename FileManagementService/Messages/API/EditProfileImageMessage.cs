using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementService.Messages.API
{
    public class EditProfileImageMessage
    {
        public string NewImage { get; set; }
        public string OldImage { get; set; }
    }
}
