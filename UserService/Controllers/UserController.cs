using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using UserService.Messages.API;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }

        [HttpPost]
        [Route("register")]
        public ApiResult RegisterUser(RegisterMessage message) 
        {
            return ApiResult.Success("success");
        }
    }
}
