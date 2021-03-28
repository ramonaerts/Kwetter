using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetService.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
    }
}
