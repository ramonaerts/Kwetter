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
        Task CreateNewTrend(Trend newTrend);
        Task UpdateTrend(Trend trend);
    }
}
