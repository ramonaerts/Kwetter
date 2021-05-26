using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LikeService.Test.Mock
{
    public interface IMongoRepositoryMock : IMongoCollection<BsonDocument>
    {
        IFindFluent<BsonDocument, BsonDocument> Find(FilterDefinition<BsonDocument> filter, FindOptions options);
    }
}
