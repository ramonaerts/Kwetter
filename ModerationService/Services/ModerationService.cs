using System.Threading.Tasks;
using ModerationService.DAL;
using ModerationService.Entities;
using ModerationService.Messages.Broker;
using MongoDB.Driver;

namespace ModerationService.Services
{
    public class ModerationService : IModerationService
    {
        private readonly IMongoCollection<Tweet> _tweets;
        private readonly IMongoCollection<User> _users;

        public ModerationService(IModerationContext context)
        {
            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Tweet>("Tweets");
            _users = database.GetCollection<User>("Users");
        }

        public async Task AddProfanityTweet(NewProfanityTweetMessage message)
        {
            var tweet = new Tweet
            {
                TweetDateTime = message.TweetDateTime,
                Id = message.Id,
                UserId = message.UserId,
                TweetContent = message.TweetContent
            };

            await _tweets.InsertOneAsync(tweet);
        }

        public async Task AddUser(NewProfileMessage message)
        {
            var user = new User
            {
                Id = message.Id,
                Nickname = message.Nickname,
                Username = message.Username,
                Image = message.Image
            };

            await _users.InsertOneAsync(user);
        }

        public async Task UpdateUser(ProfileChangedMessage message)
        {
            var user = _users.Find(u => u.Id == message.Id).FirstOrDefault();

            user.Nickname = message.Nickname;

            await _users.ReplaceOneAsync(u => u.Id == message.Id, user);
        }

        public async Task UpdateUserImage(ProfileImageChangedMessage message)
        {
            var user = _users.Find(u => u.Id == message.Id).FirstOrDefault();

            user.Image = message.Image;

            await _users.ReplaceOneAsync(u => u.Id == message.Id, user);
        }

        public async Task ForgetUser(ForgetUserMessage message)
        {
            await _users.DeleteOneAsync(u => u.Id == message.Id);
            await _tweets.DeleteManyAsync(u => u.UserId == message.Id);
        }
    }
}
