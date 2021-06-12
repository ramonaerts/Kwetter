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
    }
}
