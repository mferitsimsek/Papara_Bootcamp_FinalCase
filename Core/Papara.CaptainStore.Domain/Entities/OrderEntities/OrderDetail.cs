using Papara.CaptainStore.Domain.Entities.BaseEntities;

namespace Papara.CaptainStore.Domain.Entities.OrderEntities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
