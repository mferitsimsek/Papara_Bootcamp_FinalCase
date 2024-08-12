using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Domain.DTOs.CouponDTOs
{
    public class CouponListDTO
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public DiscountType DiscountType { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int UsedCount { get; set; }
        public int MaxUsageCount { get; set; }
    }
}
