﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimelineService.Messages.Broker
{
    public class ProfileChangedMessage
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
    }
}