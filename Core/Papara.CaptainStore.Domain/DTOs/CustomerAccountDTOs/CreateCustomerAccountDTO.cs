namespace Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs
{
    public class CreateCustomerAccountDTO
    {
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Points { get; set; }
        public Guid AppUserId { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
