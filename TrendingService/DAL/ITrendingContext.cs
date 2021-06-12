using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TrendingService.Entities;

namespace TrendingService.DAL
{
    public interface ITrendingContext
    {
        IMongoCollection<Trend> Trends { get; }
    }
}
