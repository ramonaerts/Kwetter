using AutoMapper;
using TweetService.Entities;

namespace TweetService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tweet, Models.Tweet>();
        }
    }
}
