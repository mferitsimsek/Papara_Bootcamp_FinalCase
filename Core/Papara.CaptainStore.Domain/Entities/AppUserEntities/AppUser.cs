using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Domain.Entities.AppUserEntities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Status { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
        public virtual CustomerAccount CustomerAccount { get; set; }

    }
}
