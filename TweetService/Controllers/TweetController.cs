using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TweetService.Models;
using TweetService.Services;

namespace TweetService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("tweets")]
        public ApiResult GetUserTweets()
        {
            var tweets = _tweetService.GetTweets();

            return ApiResult.Success(tweets);
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("tweet")]
        public ApiResult GetTest()
        {
            var tweet = _tweetService.GetTweet();

            return ApiResult.Success(tweet);
        }

        [HttpPost]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("")]
        public ApiResult CreateTest()
        {
            _tweetService.CreateTweet();

            return ApiResult.Success("Created");
        }
    }
}
