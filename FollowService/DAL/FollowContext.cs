namespace FollowService.DAL
{
    public class FollowContext : IFollowContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
