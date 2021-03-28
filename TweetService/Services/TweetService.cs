using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetService.Messages;
using TweetService.Models;

namespace TweetService.Services
{
    public class TweetService : ITweetService
    {
        public static List<string> names = new List<string>();

        public List<Tweet> GetTweets()
        {
            throw new NotImplementedException();
        }

        public void AddUser(User user)
        {
            names.Add(user.Username);
        }

        public List<string> GetUsers()
        {
            return names;
        }
    }
}
