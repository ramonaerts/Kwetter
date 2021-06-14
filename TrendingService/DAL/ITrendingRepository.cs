using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendingService.Entities;

namespace TrendingService.DAL
{
    public interface ITrendingRepository
    {
        List<Trend> GetAllTrends();
        Trend GetTrendByTopic(string topic);
        Task UpdateTrend(Trend trend);
    }
}
