using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace FileManagementService.Entities
{
    public class ProfileImage
    {
        [BsonId]
        public string Id { get; set; }
        public string Filename { get; set; }
        public string File { get; set; }
        public DataType DataType { get; set; }
    }
}
