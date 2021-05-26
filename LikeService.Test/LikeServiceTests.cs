using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LikeService.Entities;
using LikeService.Test.Mock;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;

namespace LikeService.Test
{
    [TestFixture]
    public class LikeServiceTests
    {
        private readonly Mock<IMongoCollection<UserLike>> _userLikeMock;
        private readonly Mock<IMongoCollection<TweetLike>> _tweetLikeMock;

        private Mock<IMongoRepositoryMock> _mongoRepoMock;
        public LikeServiceTests()
        {
            _userLikeMock = new Mock<IMongoCollection<UserLike>>();
            _tweetLikeMock = new Mock<IMongoCollection<TweetLike>>();
        }

        [OneTimeSetUp]
        public void Setup()
        {

        }

        [Test]
        public async Task CreateLikeTest()
        {

        }

        [Test]
        public void GetLikeTest()
        {
            
        }
    }
}
