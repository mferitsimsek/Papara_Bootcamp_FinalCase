namespace Papara.CaptainStore.Domain.DTOs.PaymentDTOs
{
    public class PaymentRequestDTO
    {
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string CVV { get; set; }
        public decimal Amount { get; set; }
    }
}
