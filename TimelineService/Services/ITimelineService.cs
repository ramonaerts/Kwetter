using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimelineService.Messages.Broker;

namespace TimelineService.Services
{
    public interface ITimelineService
    {
        Task GetUserTimeline(string userId);
        Task AddTweet(NewPostedTweetMessage message);
        Task FollowUser(AddFollowerMessage message);
        Task UnFollowUser(RemoveFollowerMessage message);
        Task AddUser(NewProfileMessage message);
        Task UpdateUser(ProfileChangedMessage message);
        Task UpdateUserImage(ProfileImageChangedMessage message);
    }
}
