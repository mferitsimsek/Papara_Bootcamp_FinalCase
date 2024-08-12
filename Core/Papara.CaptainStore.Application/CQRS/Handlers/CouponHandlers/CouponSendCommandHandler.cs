using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CouponCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using AutoMapper;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Application.Services.CouponServices;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class CouponSendCommandHandler : IRequestHandler<CouponSendCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;

        public CouponSendCommandHandler(IUnitOfWork unitOfWork, ICouponService couponService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _couponService = couponService;
            _mapper = mapper;
        }
        public async Task<ApiResponseDTO<object?>> Handle(CouponSendCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _unitOfWork.CouponRepository.GetByFilterAsync(c=>c.CouponCode==request.CouponCode);
                if (coupon == null)                
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Gönderilecek kupon bulunamadı." });

                var couponSendEmail= _mapper.Map<CouponSendEmailDTO>(request);
                var couponDto= _mapper.Map<CouponListDTO>(coupon);
                await _couponService.SendCouponEmailAsync(couponSendEmail, couponDto);

                return new ApiResponseDTO<object?>(200, null, new List<string> { "Kupon gönderme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kupon gönderme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
