using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using UserService.Messages.API;
using UserService.Services;

namespace UserService.Controllers
{
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
        [Route("register")]
        public ApiResult RegisterUser(RegisterMessage message) 
        {
            if(!_userService.VerifyPasswords(message.Password, message.ConfirmPassword)) return ApiResult.BadRequest("Password don't match.");

            if(!_userService.VerifyUniqueEmail(message.Email)) return ApiResult.BadRequest("This email is already in use.");

            return _userService.RegisterUser(message) ? ApiResult.Success("success") : ApiResult.BadRequest("Something went wrong.");
        }
    }
}
