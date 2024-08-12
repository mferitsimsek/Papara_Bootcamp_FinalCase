using Papara.CaptainStore.Domain.Entities.BaseEntities;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Domain.Entities.CustomerEntities
{
    public class CustomerAddress : BaseEntity
    {
        public long CustomerId { get; set; }
        public virtual Customer Customer { get; set; }


        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public int DistrictId { get; set; }
        public District? District { get; set; }
        public string AddressLine { get; set; }
        public string ZipCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
