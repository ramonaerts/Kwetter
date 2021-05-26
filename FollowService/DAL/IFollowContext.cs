using FollowService.Entities;
using MongoDB.Driver;

namespace FollowService.DAL
{
    public interface IFollowContext
    {
        IMongoCollection<Follow> Follows { get; }
    }
}
