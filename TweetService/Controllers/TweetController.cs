using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.API;

namespace TweetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController
    {
        [HttpGet]
        [Route("tweets")]
        public ApiResult GetTweets()
        {
            return ApiResult.Success("result");
        }
    }
}
