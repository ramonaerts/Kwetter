using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TweetService.Messages.Api;
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
            var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var tweets = _tweetService.GetTweets(id);

            return ApiResult.Success(tweets);
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("{id}")]
        public ApiResult GetProfileTweets(string id)
        {
            var tweets = _tweetService.GetTweets(id);

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
        [Route("create")]
        public async Task<ApiResult> CreateTweet(CreateTweetMessage message)
        {
            var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            await _tweetService.CreateTweet(id, message.TweetContent);

            return ApiResult.Success("Created");
        }
    }
}
