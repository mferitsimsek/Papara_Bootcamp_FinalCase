namespace Papara.CaptainStore.Domain.Entities.LocationEntities
{
    public class City
    {
        public int CityId { get; set; }
        public required string Definition { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public List<District> Districts { get; set; } = new List<District>();
    }
}
