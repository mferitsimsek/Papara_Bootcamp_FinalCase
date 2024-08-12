using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.ProductCommands;
using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            //CreateMap<ProductListDTO, Product>().ReverseMap();           
            CreateMap<Product, ProductListDTO>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(pc => new CategoryListDTO
            {
                Id = pc.CategoryId,
                CategoryName = pc.CategoryName,
                Url = pc.Url,
                Tag = pc.Tag
            })));

            CreateMap<ProductCreateCommandRequest, Product>().ReverseMap();
            CreateMap<ProductUpdateCommandRequest, Product>().ReverseMap();
        }
    }

}
