using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using UserService.Services;

namespace UserService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{username}")]
        public async Task<ApiResult> GetProfileByName(string username)
        {
            var user = await _profileService.getUserByUsername(username);

            return user == null ? ApiResult.NotFound("User was not found") : ApiResult.Success(user);
        }
    }
}
