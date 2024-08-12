using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.AppUserDTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUserCreateCommandRequest, AppUser>().ReverseMap();
            CreateMap<AdminUserCreateCommandRequest, AppUser>().ReverseMap();
            CreateMap<AppUser, AppUserListDTO>().ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<AppUser, AppUserLoginDTO>().ForMember(dest => dest.AppUserId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

            CreateMap<AppUserUpdateCommandRequest, AppUser>().ReverseMap();


        }
    }
}
