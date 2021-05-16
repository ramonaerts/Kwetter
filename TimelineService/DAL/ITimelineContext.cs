namespace TimelineService.DAL
{
    public interface ITimelineContext
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
