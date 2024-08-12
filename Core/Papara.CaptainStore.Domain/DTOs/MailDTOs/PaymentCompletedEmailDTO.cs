namespace Papara.CaptainStore.Domain.DTOs.MailDTOs
{
    public class PaymentCompletedEmailDTO
    {
        public string OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal PaidAmount { get; set; }
        public Guid TransactionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
    }
}
