using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using UserService.Messages.API;
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
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("{username}")]
        public async Task<ApiResult> GetProfileByName(string username)
        {
            try
            {
                var profile = await _profileService.GetProfileByUsername(username);

                if (profile == null) return ApiResult.NotFound("User was not found");

                var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                if (id == profile.Id) profile.Self = true;
                else profile.Email = null;

                return ApiResult.Success(profile);
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("@me")]
        public async Task<ApiResult> GetOwnProfile()
        {
            try
            {
                var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                var user = await _profileService.GetProfileById(id);

                return ApiResult.Success(user);
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpPut]
        [Authorize(Roles = "User,Moderator,Admin")]
        [Route("edit")]
        public async Task<ApiResult> EditProfile(EditProfileMessage message)
        {
            try
            {
                var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                if (message.Id != id) return ApiResult.Forbidden("Not allowed");

                return await _profileService.EditProfile(message) ? ApiResult.Success("Profile edited correctly") : ApiResult.BadRequest("Something went wrong");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }
    }
}
