using MongoDB.Bson.Serialization.Attributes;

namespace TweetService.Entities
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Nickname { get; set; }
        public string Image { get; set; }
    }
}
