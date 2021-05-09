using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
