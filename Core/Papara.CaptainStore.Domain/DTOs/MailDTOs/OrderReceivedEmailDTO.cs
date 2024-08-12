using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Domain.DTOs.MailDTOs
{
    public class OrderReceivedEmailDTO
    {
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal BasketTotal { get; set; }
        public decimal DiscountTotal { get; set; } // Kupon ve puan indirimi toplamı.
        public DateTime CreatedDate { get; set; }
        public ICollection<OrderDetailDTO> OrderDetailDTOs { get; set; }
    }
}
