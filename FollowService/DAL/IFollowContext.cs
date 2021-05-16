namespace FollowService.DAL
{
    public interface IFollowContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
