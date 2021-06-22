using System;
using System.Threading.Tasks;
using FollowService.Messages.Broker;
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
        public async Task FollowExistsTest()
        {
            await _followService.FollowUser("1", "2");

            var result = _followService.FollowExists("1", "2");

            Assert.True(result);
        }

        [Fact]
        public void FollowDoesNotExistsTest()
        {
            var result = _followService.FollowExists("1", "2");

            Assert.False(result);
        }

        [Fact]
        public async Task FollowUserTest()
        {
            var resultBefore = _followService.FollowExists("1", "2");

            Assert.False(resultBefore);

            await _followService.FollowUser("1", "2");

            var result = _followService.FollowExists("1", "2");

            Assert.True(result);
        }

        [Fact]
        public async Task FollowUserTestFailed()
        {
            _followService.FollowExists("1", "2");

            await _followService.FollowUser("1", "2");

            var result = await _followService.FollowUser("1", "2");

            Assert.False(result);
        }

        [Fact]
        public async Task UnFollowUserTest()
        {
            await _followService.FollowUser("1", "2");

            var resultBefore = _followService.FollowExists("1", "2");

            Assert.True(resultBefore);

            await _followService.UnFollowUser("1", "2");

            var result = _followService.FollowExists("1", "2");

            Assert.False(result);
        }

        [Fact]
        public async Task UnFollowUserTestFailed()
        {
            var result = await _followService.UnFollowUser("1", "2");

            Assert.False(result);
        }

        [Fact]
        public async Task ForgetUserTest()
        {
            await _followService.FollowUser("1", "2");
            await _followService.FollowUser("2", "1");

            var resultBeforeOne = _followService.FollowExists("1", "2");
            var resultBeforeTwo = _followService.FollowExists("2", "1");

            Assert.True(resultBeforeOne);
            Assert.True(resultBeforeTwo);

            await _followService.ForgetUser(new ForgetUserMessage { Id = "1" });

            var resultAfterOne = _followService.FollowExists("1", "2");
            var resultAfterTwo = _followService.FollowExists("2", "1");

            Assert.False(resultAfterOne);
            Assert.False(resultAfterTwo);
        }

        [Fact]
        public async Task UserFollowsHimselfTestFalse()
        {
            var result = await _followService.FollowUser("1", "1");

            Assert.False(result);
        }

        [Fact]
        public async Task GetFollowerCountTest()
        {
            for (var i = 1; i < 41; i++)
            {
                await _followService.FollowUser(i.ToString(), "0");
            }

            var followerCount = _followService.GetFollowCount("0");

            Assert.Equal(40, followerCount.FollowerCount);
        }

        [Fact]
        public async Task GetFollowingCountTest()
        {
            await _followService.FollowUser("0", "1");
            await _followService.FollowUser("0", "2");
            await _followService.FollowUser("0", "3");

            var followerCount = _followService.GetFollowCount("0");

            Assert.Equal(3, followerCount.FollowingCount);
        }

        [Fact]
        public async Task GetFollowerAndFollowingCountTest()
        {
            for (var i = 1; i < 36; i++)
            {
                await _followService.FollowUser(i.ToString(), "0");
            }

            await _followService.FollowUser("0", "1");
            await _followService.FollowUser("0", "2");

            var followerCount = _followService.GetFollowCount("0");

            Assert.Equal(35, followerCount.FollowerCount);
            Assert.Equal(2, followerCount.FollowingCount);
        }
    }
}
