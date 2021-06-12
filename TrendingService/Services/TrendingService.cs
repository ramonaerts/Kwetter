using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendingService.DAL;
using TrendingService.Entities;

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
    }
}
