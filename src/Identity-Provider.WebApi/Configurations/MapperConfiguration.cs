using AutoMapper;
using Idenitity_Provider.Persistence.Dtos.Auth;
using Identity_Provider.Domain.Entities.Users;

namespace Identity_Provider.WebApi.Configurations
{
    public class MapperConfiguration : Profile
    {
        public MapperConfiguration()
        {
            CreateMap<RegisterDto, User>().ReverseMap();
        }
    }
}
