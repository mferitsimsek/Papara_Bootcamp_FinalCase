using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.DTOs.MailDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.Services.CouponServices
{
    public interface ICouponService
    {
        Task<ApiResponseDTO<object?>?> CheckCouponIsExist(string couponCode);
        Task<ApiResponseDTO<object?>> SaveCoupon(Coupon coupon);
        Task<ApiResponseDTO<object?>> UpdateCoupon(Coupon coupon);
        Task SendCouponEmailAsync(CouponSendEmailDTO couponSendEmailDTO, CouponListDTO coupon);
    }
}
