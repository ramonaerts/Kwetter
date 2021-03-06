using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendingService.DAL;
using TrendingService.Entities;
using TrendingService.Messages.Broker;

namespace TrendingService.Services
{
    public class TrendingService : ITrendingService
    {
        private readonly ITrendingRepository _trendingRepository;

        public TrendingService(ITrendingRepository trendingRepository)
        {
            _trendingRepository = trendingRepository;
        }

        public List<Trend> GetTopTrends()
        {
            var trends = _trendingRepository.GetAllTrends();

            trends = trends.OrderByDescending(t => t.TweetCount).ToList();
            if(trends.Count > 10) trends = trends.GetRange(0, 10);

            return trends;
        }

        public async Task AddNewTopic(NewTopicTweetMessage message)
        {
            var words = message.TweetContent.Split();

            foreach (var word in words)
            {
                if (word.StartsWith("#")) await UpdateTrendCount(word);
            }
        }

        private async Task UpdateTrendCount(string topic)
        {
            topic = topic.ToLower();
            topic = "#" + char.ToUpper(topic[1]) + topic.Substring(2);

            var trend = _trendingRepository.GetTrendByTopic(topic);

            if (trend == null)
            {
                var newTrend = new Trend
                {
                    Id = Guid.NewGuid().ToString(),
                    Topic = topic,
                    TweetCount = 1
                };

                await _trendingRepository.CreateNewTrend(newTrend);
            }
            else
            {
                trend.TweetCount++;
                await _trendingRepository.UpdateTrend(trend);
            }
        }
    }
}
