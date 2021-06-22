using System;

namespace TweetService.Models
{
    public class Tweet
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public Entities.User User { get; set; }
        public string TweetContent { get; set; }
        public string TweetDateTime { get; set; }
        public bool OwnTweet { get; set; }
    }
}
