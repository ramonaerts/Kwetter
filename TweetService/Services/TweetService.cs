using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.Messaging;
using TweetService.DAL;
using TweetService.Messages.Broker;
using TweetService.Models;

namespace TweetService.Services
{
    public class TweetService : ITweetService
    {
        private readonly IMapper _mapper;
        private readonly IMessagePublisher _messagePublisher;
        private readonly IMongoCollection<Entities.Tweet> _tweets;
        private readonly IMongoCollection<Entities.User> _users;
        private readonly string _url;

        public TweetService(ITweetContext context, IMapper mapper, IMessagePublisher messagePublisher)
        {
            _mapper = mapper;
            _messagePublisher = messagePublisher;
            _url = "https://kwettermoderation.azurewebsites.net/api/CheckSwearWords?code=5/AcOOlYPIJlHOlEnQVmGYI5TxoNjfXYAMjjJee1a8YtXIOHpucY6w==";

            var client = new MongoClient(context.ConnectionString);
            var database = client.GetDatabase(context.DatabaseName);

            _tweets = database.GetCollection<Entities.Tweet>("Tweets");
            _users = database.GetCollection<Entities.User>("Users");
        }

        public List<Tweet> GetTweets(string id)
        {
            var tweets = _tweets.Find(t => t.UserId == id).ToList();

            var user = _users.Find(u => u.Id == id).FirstOrDefault();

            var tweetModels = _mapper.Map<List<Tweet>>(tweets);

            foreach (var tweet in tweetModels)
            {
                tweet.User = user;
            }

            tweetModels = tweetModels.OrderByDescending(x => x.TweetDateTime).ToList();

            return tweetModels;
        }

        public async Task AddUser(NewProfileMessage message)
        {
            var user = new Entities.User
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

        public Entities.Tweet GetTweet()
        {
            return _tweets.Find(t => t.Id == "1").FirstOrDefault();
        }

        public async Task CreateTweet(string id, string tweetContent)
        {
            var tweet = new Entities.Tweet
            {
                TweetDateTime = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                UserId = id,
                TweetContent = tweetContent
            };

            if(await CheckForProfanity(tweet))
            {
                await _messagePublisher.PublishMessageAsync("NewProfanityTweetMessage", new { TweetDateTime = tweet.TweetDateTime, Id = tweet.Id, UserId = tweet.UserId, TweetContent = tweet.TweetContent });
            }

            await _messagePublisher.PublishMessageAsync("NewPostedTweetMessage", new { TweetDateTime = tweet.TweetDateTime, Id = tweet.Id, UserId = tweet.UserId, TweetContent = tweet.TweetContent });

            _tweets.InsertOne(tweet);
        }

        public async Task<bool> CheckForProfanity(Entities.Tweet tweet)
        {
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_url),
                Content = new StringContent("{\"tweetcontent\":\"" + tweet.TweetContent + "\"}", Encoding.UTF8, "application/json")
            };

            using var response = await httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var jsonString = await response.Content.ReadAsStringAsync();

            var jObject = JsonConvert.DeserializeObject<JObject>(jsonString);

            return jObject["ProfanityResult"].Value<bool>();
        }

        public async Task ForgetUser(ForgetUserMessage message)
        {
            await _users.DeleteOneAsync(u => u.Id == message.Id);
            await _tweets.DeleteManyAsync(u => u.UserId == message.Id);
        }

        public async Task UnApproveTweet(UnApproveTweetMessage message)
        {
            await _tweets.DeleteOneAsync(t => t.Id == message.TweetId);
        }
    }
}
