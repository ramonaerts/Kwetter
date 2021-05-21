namespace TweetService.DAL
{
    public class TweetContext : ITweetContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
