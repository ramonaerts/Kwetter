using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TweetService.DAL;
using TweetService.Messages;
using TweetService.Models;

namespace TweetService.Services
{
    public class TweetService : ITweetService
    {
        public static List<User> users = new List<User>();
        public static List<Tweet> tweets = new List<Tweet>();

        private readonly IMongoCollection<Entities.Tweet> _tweets;

        public TweetService(ITweetContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Entities.Tweet>(context.CollectionName);
        }

        private static void FillMockTweets()
        {
            tweets.Add(new Tweet
            {
                TweetId = 1,
                TweetContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                UserId = 1,
                TweetDateTime = DateTime.Now.AddHours(-4)
            });
            tweets.Add(new Tweet
            {
                TweetId = 2,
                TweetContent = "Test Tweet.",
                UserId = 1,
                TweetDateTime = DateTime.Now.AddHours(-2)
            });
            tweets.Add(new Tweet
            {
                TweetId = 3,
                TweetContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                UserId = 1,
                TweetDateTime = DateTime.Now.AddHours(-1)
            });
            tweets.Add(new Tweet
            {
                TweetId = 4,
                TweetContent = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                UserId = 1,
                TweetDateTime = DateTime.Now.AddHours(-1)
            });
        }

        public List<Tweet> GetTweets()
        {
            if (tweets.Count == 0) FillMockTweets();

            foreach (var tweet in tweets) tweet.User = users.Find(e => e.UserId == tweet.UserId);

            return tweets;
        }

        public void AddUser(UserChange user)
        {
            users.Add(new User{UserId = user.UserId, Username = user.Username, Nickname = user.Nickname});
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public Entities.Tweet GetTweet()
        {
            return _tweets.Find(t => t.Id == "1").FirstOrDefault();
        }

        public void CreateTweet()
        {
            Entities.Tweet tweet = new Entities.Tweet
            {
                TweetDateTime = DateTime.Now,
                Id = "1c440290-febf-4d0e-81b6-1bcaac7d1b76",
                TweetContent = "test"
            };

            _tweets.InsertOne(tweet);
        }
    }
}
