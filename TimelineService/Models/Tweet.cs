using System;
using TimelineService.Entities;

namespace TimelineService.Models
{
    public class Tweet
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string TweetContent { get; set; }
        public DateTime TweetDateTime { get; set; }
    }
}
