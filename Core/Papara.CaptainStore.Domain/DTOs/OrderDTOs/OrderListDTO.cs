namespace Papara.CaptainStore.Domain.DTOs.OrderDTOs
{
    public class OrderListDTO
    {
        public int Id { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal CouponDiscountAmount { get; set; }
        public string CouponCode { get; set; }
        public decimal PointsTotal { get; set; }
        public string OrderNumber { get; set; }
        public bool PaymentCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid UpdatedUserId { get; set; }
        public ICollection<OrderDetailDTO> OrderDetails { get; set; }
    }
}
