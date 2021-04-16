using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TweetService.Models;
using TweetService.Services;

namespace TweetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet]
        [Route("tweets")]
        public ApiResult GetUserTweets()
        {
            List<Tweet> tweets = _tweetService.GetTweets();

            return ApiResult.Success(tweets);
        }

        [HttpGet]
        [Route("test")]
        public ApiResult GetTest()
        {
            Entities.Tweet tweet = _tweetService.GetTweet();

            return ApiResult.Success(tweet);
        }

        [HttpPost]
        [Route("test")]
        public ApiResult CreateTest()
        {
            _tweetService.CreateTweet();

            return ApiResult.Success("yur");
        }
    }
}
