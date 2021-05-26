using System.Threading.Tasks;
using FollowService.Messages.Broker;

namespace FollowService.Services
{
    public interface IFollowService
    {
        bool FollowExists(string followerId, string followingId);
        Task<bool> FollowUser(string followerId, string followingId);
        Task<bool> UnFollowUser(string followerId, string followingId);
        Task ForgetUser(ForgetUserMessage message);
    }
}
