using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TimelineService.Services;

namespace TimelineService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TimelineController : ControllerBase
    {
        private readonly ITimelineService _timelineService;

        public TimelineController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("")]
        public async Task<ApiResult> GetUserTimeline()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

            await _timelineService.GetUserTimeline(userId);

            return ApiResult.Success("success");
        }
    }
}
