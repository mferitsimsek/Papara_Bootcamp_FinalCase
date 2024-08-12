namespace Papara.CaptainStore.Domain.DTOs
{
    public class AppUserLoginDTO
    {
        public Guid AppUserId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool? Status { get; set; }

        public required string Token { get; set; }
        public DateTime TokenExpiredDateTime { get; set; }
    }
}
