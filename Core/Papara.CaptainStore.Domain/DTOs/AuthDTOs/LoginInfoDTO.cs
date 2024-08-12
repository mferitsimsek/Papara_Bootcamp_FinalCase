namespace Papara.CaptainStore.Domain.DTOs
{
    public class LoginInfoDTO
    {
        public required string Token { get; set; }
        public DateTime TokenExpiredDateTime { get; set; }
    }
}
