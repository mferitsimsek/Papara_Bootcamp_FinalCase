namespace Papara.CaptainStore.Domain.Entities.LocationEntities
{
    public class Country
    {
        public int CountryId { get; set; }
        public required string Rewrite { get; set; }
        public required string Definition { get; set; }
        public required string Code { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}
