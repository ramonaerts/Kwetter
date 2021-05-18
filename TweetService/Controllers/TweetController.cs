using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TweetService.Messages.Api;
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
        [AllowAnonymous]
        [Route("profanity")]
        public async Task<ApiResult> GetTest()
        {
            var tweet = new Entities.Tweet
            {
                Id = "1",
                TweetContent = "this is a test tweet",
                TweetDateTime = DateTime.Now,
                UserId = "11"
            };

            var result = await _tweetService.CheckForProfanity(tweet);

            return ApiResult.Success("test result");
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
