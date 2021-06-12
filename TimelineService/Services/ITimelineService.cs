using System.Collections.Generic;
using System.Threading.Tasks;
using TimelineService.Messages.Broker;
using TimelineService.Models;

namespace TimelineService.Services
{
    public interface ITimelineService
    {
        List<Tweet> GetUserTimeline(string userId);
        Task AddTweet(NewPostedTweetMessage message);
        Task FollowUser(AddFollowerMessage message);
        Task UnFollowUser(RemoveFollowerMessage message);
        Task AddUser(NewProfileMessage message);
        Task UpdateUser(ProfileChangedMessage message);
        Task UpdateUserImage(ProfileImageChangedMessage message);
        Task ForgetUser(ForgetUserMessage message);
        Task UnApproveTweet(UnApproveTweetMessage message);
    }
}
