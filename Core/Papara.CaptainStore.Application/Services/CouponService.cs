using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.Services
{
    public class CouponService : BaseService<Coupon>
    {
        public CouponService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponseDTO<object?>?> CheckCouponIsExist(string couponCode)
        {
            return await CheckEntityExists(c => c.CouponCode == couponCode, "Eklemek istediğiniz kupon sistemde kayıtlıdır.");
        }
        public async Task<ApiResponseDTO<object?>> SaveCoupon(Coupon coupon)
        {
            return await SaveEntity(coupon);
        }
    }
}
