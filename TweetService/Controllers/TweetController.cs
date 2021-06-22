using System;
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
        [Route("{id}")]
        public ApiResult GetProfileTweets(string id)
        {
            try
            {
                var currentUserId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                var tweets = _tweetService.GetTweets(id, currentUserId);

                return ApiResult.Success(tweets);
            }
            catch (Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("create")]
        public async Task<ApiResult> CreateTweet(CreateTweetMessage message)
        {
            try
            {
                var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                await _tweetService.CreateTweet(id, message.TweetContent);

                return ApiResult.Success("Created");
            }
            catch (Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("{id}")]
        public async Task<ApiResult> DeleteTweet(string id)
        {
            try
            {
                var userId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();
                var roleString = User.Claims.First(c => c.Type == ClaimTypes.Role).Value.ToString();

                var result = await _tweetService.DeleteTweet(id, userId, roleString);

                return result ? ApiResult.Success("Tweet deleted") : ApiResult.NotFound("Something went wrong");
            }
            catch (Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }
    }
}
