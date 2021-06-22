using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TrendingService.Services;

namespace TrendingService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TrendingController : ControllerBase
    {
        private readonly ITrendingService _trendingService;

        public TrendingController(ITrendingService trendingService)
        {
            _trendingService = trendingService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("top")]
        public ApiResult GetTopTrends()
        {
            try
            {
                var topTrends = _trendingService.GetTopTrends();

                return ApiResult.Success(topTrends);
            }
            catch (Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }
    }
}
