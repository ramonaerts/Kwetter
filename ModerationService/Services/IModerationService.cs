using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModerationService.Messages.Broker;

namespace ModerationService.Services
{
    public interface IModerationService
    {
        Task<bool> ApproveProfanityTweet(string tweetId);
        Task<bool> UnApproveProfanityTweet(string tweetId);
        Task AddProfanityTweet(NewProfanityTweetMessage message);
        Task AddUser(NewProfileMessage message);
        Task UpdateUser(ProfileChangedMessage message);
        Task UpdateUserImage(ProfileImageChangedMessage message);
        Task ForgetUser(ForgetUserMessage message);
    }
}
