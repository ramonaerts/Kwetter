using System.Collections.Generic;
using System.Threading.Tasks;
using TweetService.Messages.Broker;
using TweetService.Models;

namespace TweetService.Services
{
    public interface ITweetService
    {
        List<Tweet> GetTweets(string id);
        Task AddUser(NewProfileMessage message);
        Task UpdateUser(ProfileChangedMessage message);
        Task UpdateUserImage(ProfileImageChangedMessage message);
        Entities.Tweet GetTweet();
        Task CreateTweet(string id, string tweetContent);
        Task<bool> CheckForProfanity(Entities.Tweet tweet);
        Task ForgetUser(ForgetUserMessage message);
        Task UnApproveTweet(UnApproveTweetMessage message);
    }
}
