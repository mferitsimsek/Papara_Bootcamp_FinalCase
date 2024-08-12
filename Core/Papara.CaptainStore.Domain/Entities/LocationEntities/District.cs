namespace Papara.CaptainStore.Domain.Entities.LocationEntities
{
    public class District
    {
        public int DistrictId { get; set; }
        public required string Definition { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
    }
}
