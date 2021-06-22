using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileManagementService.DAL.Config;
using FileManagementService.Entities;
using MongoDB.Driver;

namespace FileManagementService.DAL
{
    public class FileManagementContext : IFileManagementContext
    {
        private readonly IMongoDatabase _db;

        public FileManagementContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<ProfileImage> ProfileImages => _db.GetCollection<ProfileImage>("ProfileImages");
    }
}
