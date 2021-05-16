namespace UserService.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public bool Verified { get; set; }
    }
}
