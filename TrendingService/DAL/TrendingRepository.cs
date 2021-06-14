using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TrendingService.Entities;

namespace TrendingService.DAL
{
    public class TrendingRepository : ITrendingRepository
    {
        private readonly ITrendingContext _context;

        public TrendingRepository(ITrendingContext context)
        {
            _context = context;
        }

        public List<Trend> GetAllTrends()
        {
            return _context.Trends.AsQueryable().ToList();
        }

        public Trend GetTrendByTopic(string topic)
        {
            var trends = GetAllTrends();

            return trends.FirstOrDefault(t => t.Topic == topic);
        }

        public async Task UpdateTrend(Trend trend)
        {
            await _context.Trends.ReplaceOneAsync(t => t.Id == trend.Id, trend);
        }
    }
}
