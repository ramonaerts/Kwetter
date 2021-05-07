using System;
using System.Collections.Generic;
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
        [AllowAnonymous]
        [Route("follow")]
        public async Task<ApiResult> PostFollower(FollowMessage message)
        {
            var followerId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = await _followService.FollowUser(followerId, message.Id);

            return result ? ApiResult.Success("Followed successfully") : ApiResult.BadRequest("Something went wrong");
        }

        [HttpDelete]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<ApiResult> DeleteFollower(string id)
        {
            var followerId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = await _followService.UnFollowUser(followerId, id);

            return result ? ApiResult.Success("followed successfully") : ApiResult.BadRequest("Something went wrong");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public async Task<ApiResult> CheckIfFollows(string id)
        {
            var followerId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = await _followService.FollowExists(followerId, id);

            return ApiResult.Success(result);
        }
    }
}
