using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs
{
    public class CustomerAccountListDTO
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Points { get; set; }
        public Guid AppUserId { get; set; }
        [JsonIgnore]
        public virtual AppUser AppUser { get; set; }

    }
}
