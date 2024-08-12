namespace Papara.CaptainStore.Domain.DTOs
{
    public class CountryListDTO
    {
        public int CountryId { get; set; }
        public required string Rewrite { get; set; }
        public required string Definition { get; set; }
        public required string Code { get; set; }
        public List<CityListDTO> Cities { get; set; } = new List<CityListDTO>();
    }
}
