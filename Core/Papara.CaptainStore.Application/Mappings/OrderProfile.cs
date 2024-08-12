using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;
using Papara.CaptainStore.Domain.Entities.OrderEntities;

namespace Papara.CaptainStore.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderCreateCommandRequest, Order>().ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails));
            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
            CreateMap<OrderListDTO, Order>().ReverseMap();
        }
    }
}

