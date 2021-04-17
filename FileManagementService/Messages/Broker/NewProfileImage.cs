using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileManagementService.Messages.Broker
{
    public class NewProfileImage
    {
        public string Image { get; set; }
        public string ImageName { get; set; }
    }
}
