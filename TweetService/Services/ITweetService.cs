using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetService.Messages;
using TweetService.Messages.Broker;
using TweetService.Models;

namespace TweetService.Services
{
    public interface ITweetService
    {
        List<Tweet> GetTweets(string id);
        void AddUser(NewProfileMessage user);
        void UpdateUser(ProfileChangedMessage message);
        void UpdateUserImage(ProfileImageChangedMessage message);
        Entities.Tweet GetTweet();
        void CreateTweet(string id, string tweetContent);
    }
}
