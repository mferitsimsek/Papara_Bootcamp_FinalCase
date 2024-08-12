using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            CreateMap<AppRoleListDTO, AppRole>().ForMember(
                x => x.Id, x => x.MapFrom(x => x.AppRoleId)
                ).ReverseMap();
            CreateMap<AppRoleCreateCommandRequest, AppRole>().ReverseMap();
            CreateMap<AppRoleUpdateCommandRequest, AppRole>().ReverseMap();


        }
    }
}
