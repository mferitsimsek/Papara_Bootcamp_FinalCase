using Papara.CaptainStore.Domain.Entities.BaseEntities;
using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Domain.Entities.CouponEntities;

public class Coupon : BaseEntity
{
    public string CouponCode { get; set; }
    public decimal DiscountAmount { get; set; }
    public DiscountType DiscountType { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public int UsedCount { get; set; }
    public int MaxUsageCount { get; set; }
}

