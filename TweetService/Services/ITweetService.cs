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
        List<Tweet> GetTweets();
        void AddUser(NewProfileMessage user);
        List<User> GetUsers();
        Entities.Tweet GetTweet();
        void CreateTweet();
    }
}
