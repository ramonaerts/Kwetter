using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FollowService.Messages.API;
using FollowService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;

namespace FollowService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }

        [HttpPost]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("follow")]
        public async Task<ApiResult> PostFollower(FollowMessage message)
        {
            var followerId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = await _followService.FollowUser(followerId, message.Id);

            return result ? ApiResult.Success("Followed successfully") : ApiResult.BadRequest("Something went wrong");
        }

        [HttpDelete]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("{id}")]
        public async Task<ApiResult> DeleteFollower(string id)
        {
            var followerId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = await _followService.UnFollowUser(followerId, id);

            return result ? ApiResult.Success("followed successfully") : ApiResult.BadRequest("Something went wrong");
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("{id}")]
        public ApiResult CheckIfFollows(string id)
        {
            var followerId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = _followService.FollowExists(followerId, id);

            return ApiResult.Success(result);
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("count/{id}")]
        public ApiResult GetFollowCounts(string id)
        {
            var result = _followService.GetFollowCount(id);

            return ApiResult.Success(result);
        }
    }
}
