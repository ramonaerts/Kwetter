using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LikeService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;

namespace LikeService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("{tweetId}")]
        public async Task<ApiResult> LikeTweet(string tweetId)
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            var result = await _likeService.NewLike(userId, tweetId);

            return ApiResult.Success("Success");
        }
    }
}
