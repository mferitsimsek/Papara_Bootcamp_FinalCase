using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using Papara.CaptainStore.Domain.Entities.BaseEntities;

namespace Papara.CaptainStore.Domain.Entities.CustomerEntities
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Email { get; set; }
        public int CustomerNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }

        public virtual AppUser AppUser { get; set; }
        public virtual CustomerDetail CustomerDetail { get; set; }
        public virtual List<CustomerAddress> CustomerAddresses { get; set; }
        //public virtual List<CustomerPhone> CustomerPhones { get; set; }
        //public virtual Account Account { get; set; }
    }
}
