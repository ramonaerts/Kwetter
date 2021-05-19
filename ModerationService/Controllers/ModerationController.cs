using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;

namespace ModerationService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ModerationController : ControllerBase
    {
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
