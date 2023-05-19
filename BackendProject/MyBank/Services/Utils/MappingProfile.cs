using AutoMapper;
using Models.Domain.Entites;
using Models.DTO.User;

namespace Services.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RegisterUser, SignupRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Entites.User, SignupRequestDTO>().ReverseMap();
            CreateMap<RegisterUser, Models.Domain.Entites.User>().ReverseMap();
            CreateMap<Models.Domain.Entites.User, AuthenticateRequestDTO>().ReverseMap();
            CreateMap<Models.Domain.Entites.User, AuthenticatedUserDTO>().ReverseMap();
        }
    }
}
