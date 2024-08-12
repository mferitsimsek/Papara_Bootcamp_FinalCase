using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.Interfaces.CouponServices
{
    public interface ICouponService
    {
        Task<ApiResponseDTO<object?>?> CheckCouponIsExist(string couponCode);
        Task<ApiResponseDTO<object?>> SaveCoupon(Coupon coupon);
        Task<ApiResponseDTO<object?>> UpdateCoupon(Coupon coupon);
    }
}
