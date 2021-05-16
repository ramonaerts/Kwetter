using AutoMapper;
using UserService.Entities;

namespace UserService
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, Models.Profile>();
        }
    }
}
