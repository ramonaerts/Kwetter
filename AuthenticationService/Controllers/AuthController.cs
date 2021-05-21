using AuthenticationService.Messages.Api;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.API;

namespace AuthenticationService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/{controller}")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ApiResult Login([FromBody] LoginMessage message)
        {
            var user = _authService.LoginUser(message);
            if (user == null) return ApiResult.Forbidden("Wrong credentials");

            var token = _authService.CreateToken(user.Id, user.Role);

            return ApiResult.Success(user, token);
        }
    }
}
