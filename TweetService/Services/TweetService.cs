using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TweetService.DAL;
using TweetService.Messages;
using TweetService.Messages.Broker;
using TweetService.Models;

namespace TweetService.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMongoCollection<Entities.Tweet> _tweets;
        private readonly IMongoCollection<Entities.User> _users;

        public TweetService(ITweetContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Entities.Tweet>("Tweets");
            _users = database.GetCollection<Entities.User>("Users");
        }

        public List<Tweet> GetTweets()
        {
            throw new NotImplementedException();
        }

        public void AddUser(NewProfileMessage message)
        {
            var user = new Entities.User
            {
                Id = message.Id,
                Nickname = message.Nickname,
                Username = message.Username,
                Image = message.Image
            };

            _users.InsertOne(user);
        }

        public List<User> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Entities.Tweet GetTweet()
        {
            return _tweets.Find(t => t.Id == "1").FirstOrDefault();
        }

        public void CreateTweet()
        {
            var tweet = new Entities.Tweet
            {
                TweetDateTime = DateTime.Now,
                Id = "1c440290-febf-4d0e-81b6-1bcaac7d1b76",
                TweetContent = "test"
            };

            _tweets.InsertOne(tweet);
        }
    }
}
