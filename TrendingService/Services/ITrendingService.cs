using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrendingService.Entities;
using TrendingService.Messages.Broker;

namespace TrendingService.Services
{
    public interface ITrendingService
    {
        //test
        List<Trend> GetTopTrends();
        Task AddNewTopic(NewTopicTweetMessage message);
    }
}
