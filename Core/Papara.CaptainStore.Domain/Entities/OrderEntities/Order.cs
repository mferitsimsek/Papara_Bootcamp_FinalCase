using Papara.CaptainStore.Domain.Entities.BaseEntities;

namespace Papara.CaptainStore.Domain.Entities.OrderEntities
{
    public class Order:BaseEntity
    {
        public int Id { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal CouponDiscountAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsTotal { get; set; }
        public string OrderNumber { get; set; }
        public bool PaymentCompleted{ get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
