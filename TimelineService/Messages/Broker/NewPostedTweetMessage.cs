using System;

namespace TimelineService.Messages.Broker
{
    public class NewPostedTweetMessage
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string TweetContent { get; set; }
        public string TweetDateTime { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
