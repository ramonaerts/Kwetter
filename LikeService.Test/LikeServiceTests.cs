using System;
using System.Threading.Tasks;
using LikeService.Messages.Broker;
using LikeService.Test.Mock;
using Moq;
using Xunit;

namespace LikeService.Test
{
    public class LikeServiceTests
    {
        private readonly Services.LikeService _likeService;

        public LikeServiceTests()
        {
            var mongoRepositoryMock = new MongoRepositoryMock();
            _likeService = new Services.LikeService(mongoRepositoryMock);
        }

        [Fact]
        public void GetNoLikesTest()
        {
            var result = _likeService.GetLikes("1", "1");

            Assert.Equal(0, result.LikeCount);
            Assert.False(result.Liked);
        }

        [Fact]
        public async Task GetLikedTest()
        {
            await _likeService.NewLike("1", "1");

            var result = _likeService.GetLikes("1", "1");

            Assert.Equal(1, result.LikeCount);
            Assert.True(result.Liked);
        }

        [Fact]
        public async Task GetNotLikedTest()
        {
            await _likeService.NewLike("2", "1");

            var result = _likeService.GetLikes("1", "1");

            Assert.Equal(1, result.LikeCount);
            Assert.False(result.Liked);
        }

        [Fact]
        public async Task GetMultipleLikesLikedTest()
        {
            for (var i = 1; i < 11; i++)
            {
                await _likeService.NewLike(i.ToString(), "1");
            }

            var result = _likeService.GetLikes("1", "1");

            Assert.Equal(10, result.LikeCount);
            Assert.True(result.Liked);
        }

        [Fact]
        public async Task GetMultipleLikesNotLikedTest()
        {
            for (var i = 2; i < 12; i++)
            {
                await _likeService.NewLike(i.ToString(), "1");
            }

            var result = _likeService.GetLikes("1", "1");

            Assert.Equal(10, result.LikeCount);
            Assert.False(result.Liked);
        }

        [Fact]
        public async Task UnLikeTweetTest()
        {
            await _likeService.NewLike("1", "1");

            var resultBefore = _likeService.GetLikes("1", "1");

            Assert.Equal(1, resultBefore.LikeCount);
            Assert.True(resultBefore.Liked);

            await _likeService.RemoveLike("1", "1");

            var resultAfter = _likeService.GetLikes("1", "1");

            Assert.Equal(0, resultAfter.LikeCount);
            Assert.False(resultAfter.Liked);
        }

        [Fact]
        public async Task UnLikeNotLikedTweetTestFailed()
        {
            await _likeService.NewLike("2", "1");

            var resultBefore = _likeService.GetLikes("1", "1");

            Assert.Equal(1, resultBefore.LikeCount);
            Assert.False(resultBefore.Liked);

            var resultFail = await _likeService.RemoveLike("1", "1");

            Assert.False(resultFail);
        }

        [Fact]
        public async Task UnLikeTweetTestSucceeded()
        {
            await _likeService.NewLike("1", "1");
            var result = await _likeService.RemoveLike("1", "1");

            Assert.True(result);
        }

        [Fact]
        public async Task UnLikeTweetTestFailed()
        {
            var result = await _likeService.RemoveLike("1", "1");

            Assert.False(result);
        }

        [Fact]
        public async Task ForgetUserTest()
        {
            await _likeService.NewLike("1", "1");

            await _likeService.ForgetUser(new ForgetUserMessage {Id = "1"});

            var result = _likeService.GetLikes("1", "1");

            Assert.Equal(1, result.LikeCount);
        }
    }
}
