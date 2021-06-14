﻿using System;
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

            trends = trends.OrderBy(t => t.TweetCount).ToList();

            return trends.GetRange(0, 10);
        }

        public async Task AddNewTopic(NewTopicTweetMessage message)
        {
            var punctuation = message.TweetContent.Where(char.IsPunctuation).Distinct().ToArray();
            var words = message.TweetContent.Split().Select(x => x.Trim(punctuation));

            foreach (var word in words)
            {
                if (word.StartsWith("#")) await UpdateTrendCount(word);
            }
        }

        private async Task UpdateTrendCount(string topic)
        {
            var trend = _trendingRepository.GetTrendByTopic(topic);

            trend.TweetCount++;

            await _trendingRepository.UpdateTrend(trend);
        }
    }
}