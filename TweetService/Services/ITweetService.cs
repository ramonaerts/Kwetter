using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetService.Models;

namespace TweetService.Services
{
    public interface ITweetService
    {
        List<Tweet> GetTweets();
    }
}
