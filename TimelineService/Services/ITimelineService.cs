using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimelineService.Services
{
    public interface ITimelineService
    {
        Task GetUserTimeline(string userId);
    }
}
