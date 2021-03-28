using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared.API;
using TweetService.Services;

namespace TweetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController
    {
        private readonly ITweetService _tweetService;
        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet]
        [Route("tweets")]
        public ApiResult GetTweets()
        {
            return ApiResult.Success("result");
        }

        [HttpGet]
        [Route("test")]
        public ApiResult GetTest()
        {
            List<string> testUsers = _tweetService.GetUsers();

            return ApiResult.Success(testUsers);
        }
    }
}
