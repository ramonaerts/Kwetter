using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendingService.Entities;

namespace TrendingService.Services
{
    public interface ITrendingService
    {
        List<Trend> GetTopTrends();
    }
}
