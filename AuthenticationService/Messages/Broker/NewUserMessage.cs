namespace AuthenticationService.Messages.Broker
{
    public class NewUserMessage
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
