using System;
using FollowService.Test.Mock;
using Xunit;

namespace FollowService.Test
{
    public class FollowServiceTests
    {
        private readonly Services.FollowService _followService;

        public FollowServiceTests()
        {
            var mongoRepositoryMock = new MongoRepositoryMock();
            _followService = new Services.FollowService(new MessagePublisherMock(), mongoRepositoryMock);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
