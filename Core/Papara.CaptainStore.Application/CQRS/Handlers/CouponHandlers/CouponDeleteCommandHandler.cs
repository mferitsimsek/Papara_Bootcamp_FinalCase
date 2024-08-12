using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CouponCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class CouponDeleteCommandHandler : IRequestHandler<CouponDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CouponService _couponService;

        public CouponDeleteCommandHandler(IUnitOfWork unitOfWork, CouponService couponService)
        {
            _unitOfWork = unitOfWork;
            _couponService = couponService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(CouponDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(request.CouponId);
                if (coupon == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek kupon bulunamadı." });
                }
                coupon.IsDeleted = true;
                await _couponService.UpdateEntity(coupon);

                return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
