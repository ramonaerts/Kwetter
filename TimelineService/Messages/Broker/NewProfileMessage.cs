namespace TimelineService.Messages.Broker
{
    public class NewProfileMessage
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Image { get; set; }
    }
}
