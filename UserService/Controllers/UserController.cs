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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<ApiResult> RegisterUser(RegisterMessage message) 
        {
            try
            {
                if (!_userService.VerifyPasswords(message.Password, message.ConfirmPassword)) return ApiResult.BadRequest("Password don't match.");

                if (_userService.VerifyUniqueEmail(message.Email)) return ApiResult.BadRequest("This email is already in use.");

                return await _userService.RegisterUser(message) ? ApiResult.Success("success") : ApiResult.BadRequest("Something went wrong.");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("admin")]
        public async Task<ApiResult> CreateAdmin(RegisterMessage message)
        {
            try
            {
                if (!_userService.VerifyPasswords(message.Password, message.ConfirmPassword)) return ApiResult.BadRequest("Password don't match.");

                if (_userService.VerifyUniqueEmail(message.Email)) return ApiResult.BadRequest("This email is already in use.");

                return await _userService.CreateAdmin(message) ? ApiResult.Success("success") : ApiResult.BadRequest("Something went wrong.");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        [Route("forget")]
        public async Task<ApiResult> ForgetUser()
        {
            try
            {
                var id = User.Claims.First(c => c.Type == ClaimTypes.Name).Value.ToString();

                var result = await _userService.ForgetUser(id);

                return result ? ApiResult.Success("Account deleted") : ApiResult.NotFound("User could not be found");
            }
            catch (System.Exception)
            {
                return ApiResult.BadRequest("Something went wrong");
            }
        }
    }
}
