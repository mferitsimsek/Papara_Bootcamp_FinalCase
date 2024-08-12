using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Domain.Entities.BaseEntities;

namespace Papara.CaptainStore.Domain.Entities.CustomerEntities
{
    public class CustomerAccount : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public decimal Points { get; set; }
    }
}
