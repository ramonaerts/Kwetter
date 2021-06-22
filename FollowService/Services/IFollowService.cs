using System.Threading.Tasks;
using FollowService.Messages.Broker;
using FollowService.Models;

namespace FollowService.Services
{
    public interface IFollowService
    {
        bool FollowExists(string followerId, string followingId);
        FollowCount GetFollowCount(string userId);
        Task<bool> FollowUser(string followerId, string followingId);
        Task<bool> UnFollowUser(string followerId, string followingId);
        Task ForgetUser(ForgetUserMessage message);
    }
}
