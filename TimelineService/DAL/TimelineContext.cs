namespace TimelineService.DAL
{
    public class TimelineContext : ITimelineContext
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
    }
}
