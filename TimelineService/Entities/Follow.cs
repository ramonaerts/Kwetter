using MongoDB.Bson.Serialization.Attributes;

namespace TimelineService.Entities
{
    public class Follow
    {
        [BsonId]
        public string Id { get; set; }
        public string Follower { get; set; }
        public string Following { get; set; }
    }
}
