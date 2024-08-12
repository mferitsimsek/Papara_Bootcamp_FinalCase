using Papara.CaptainStore.Domain.DTOs.AppUserDTOs;

namespace Papara.CaptainStore.Domain.DTOs.MailDTOs
{
    public class AccountCreatedEmailDTO
    {
        public Guid AppUserId { get; set; }
        public virtual AppUserListDTO AppUser { get; set; }

        public int AccountNumber { get; set; }
    }
}
