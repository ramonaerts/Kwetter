namespace TimelineService.Messages.Broker
{
    public class RemoveFollowerMessage
    {
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }
    }
}
