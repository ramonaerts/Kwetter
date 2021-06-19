using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModerationService.Messages.Broker;
using ModerationService.Models;

namespace ModerationService.Services
{
    public interface IModerationService
    {
        Task<bool> ApproveProfanityTweet(string tweetId);
        Task<bool> UnApproveProfanityTweet(string tweetId);
        Task<bool> UpgradeUserToModerator(string userId);
        List<Tweet> GetProfanityTweetsByStatus(Status status);
        Task AddProfanityTweet(NewProfanityTweetMessage message);
        Task AddUser(NewProfileMessage message);
        Task UpdateUser(ProfileChangedMessage message);
        Task UpdateUserImage(ProfileImageChangedMessage message);
        Task ForgetUser(ForgetUserMessage message);
        Task DeleteTweet(string tweetId);
    }
}
