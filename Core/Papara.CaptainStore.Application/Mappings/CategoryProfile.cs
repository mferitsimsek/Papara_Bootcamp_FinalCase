using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands;
using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryListDTO, Category>().ReverseMap();
            CreateMap<CategoryCreateCommandRequest, Category>().ReverseMap();
            CreateMap<CategoryUpdateCommandRequest, Category>().ReverseMap();
        }
    }
}
