using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Domain.DTOs.MailDTOs
{
    public class CouponSendEmailDTO
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CouponCode { get; set; }
        public string UserId { get; set; }
        public string CouponValidityDate { get; set; }
        public DiscountType DiscountType{ get; set; }
        public decimal DiscountAmount{ get; set; }
    }
}
