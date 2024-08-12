using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Domain.DTOs.OrderDTOs
{
    public class OrderCouponDetailsResultDTO
    {
        public bool IsSuccess { get; set; }
        public Coupon Coupon { get; set; }
        public decimal CouponDiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public List<string> Errors { get; set; }
    }
}
