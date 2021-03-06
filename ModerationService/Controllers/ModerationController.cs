using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModerationService.Models;
using ModerationService.Services;
using Shared.API;

namespace ModerationService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ModerationController : ControllerBase
    {
        private readonly IModerationService _moderationService;

        public ModerationController(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        [Route("pending")]
        public ApiResult GetPendingTweets()
        {
            try
            {
                var tweets = _moderationService.GetProfanityTweetsByStatus(Status.Pending);

                return ApiResult.Success(tweets);
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        [Route("approved")]
        public ApiResult GetApprovedTweets()
        {
            try
            {
                var tweets = _moderationService.GetProfanityTweetsByStatus(Status.Approved);

                return ApiResult.Success(tweets);
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator")]
        [Route("unapproved")]
        public ApiResult GetUnapprovedTweets()
        {
            try
            {
                var tweets = _moderationService.GetProfanityTweetsByStatus(Status.Unapproved);

                return ApiResult.Success(tweets);
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Moderator")]
        [Route("approve/{tweetId}")]
        public async Task<ApiResult> ApproveTweet(string tweetId)
        {
            try
            {
                var result = await _moderationService.ApproveProfanityTweet(tweetId);

                return result ? ApiResult.Success("Tweet has been approved.") : ApiResult.NotFound("Could not find Tweet.");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Moderator")]
        [Route("unapprove/{tweetId}")]
        public async Task<ApiResult> UnApproveTweet(string tweetId)
        {
            try
            {
                var result = await _moderationService.UnApproveProfanityTweet(tweetId);

                return result ? ApiResult.Success("Tweet has been approved.") : ApiResult.NotFound("Could not find Tweet.");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Moderator,Admin")]
        [Route("upgrade/{userId}")]
        public async Task<ApiResult> UpgradeUserToModerator(string userId)
        {
            try
            {
                var result = await _moderationService.UpgradeUserToModerator(userId);

                return result ? ApiResult.Success("User had been upgraded to admin.") : ApiResult.NotFound("Could not find user.");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }
    }
}
