using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPut]
        [Authorize(Roles = "Moderator")]
        [Route("approve/{tweetId}")]
        public async Task<ApiResult> ApproveTweet(string tweetId)
        {
            var result = await _moderationService.ApproveProfanityTweet(tweetId);

            return result ? ApiResult.Success("Tweet has been approved.") : ApiResult.NotFound("Could not find Tweet.");
        }

        [HttpPut]
        [Authorize(Roles = "Moderator")]
        [Route("unapprove/{tweetId}")]
        public async Task<ApiResult> UnApproveTweet(string tweetId)
        {
            var result = await _moderationService.UnApproveProfanityTweet(tweetId);

            return result ? ApiResult.Success("Tweet has been approved.") : ApiResult.NotFound("Could not find Tweet.");
        }

        [HttpPut]
        [Authorize(Roles = "Moderator,Admin")]
        [Route("upgrade")]
        public ApiResult UpgradeUserToModerator()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            return ApiResult.Success(userId);
        }
    }
}
