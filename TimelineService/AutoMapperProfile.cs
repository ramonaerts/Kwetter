using AutoMapper;
using TimelineService.Entities;

namespace TimelineService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tweet, Models.Tweet>();
        }
    }
}
