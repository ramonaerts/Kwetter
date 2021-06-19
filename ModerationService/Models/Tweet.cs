using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModerationService.Entities;

namespace ModerationService.Models
{
    public class Tweet
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
        public Status TweetStatus { get; set; }
    }
}
