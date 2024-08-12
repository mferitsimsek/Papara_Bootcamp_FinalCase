using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.CouponCommands;
using Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.Mappings
{

    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<CouponCreateCommandRequest, Coupon>().ReverseMap();
            CreateMap<CouponUpdateCommandRequest, Coupon>().ReverseMap();
            CreateMap<CouponListDTO, Coupon>().ReverseMap();
            CreateMap<CouponSendCommandRequest, CouponSendEmailDTO>().ReverseMap();
        }
    }
}
