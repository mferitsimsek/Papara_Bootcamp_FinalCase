namespace Papara.CaptainStore.Domain.DTOs
{
    public class CityListDTO
    {
        public int CityId { get; set; }
        public required string Definition { get; set; }
        public int CountryId { get; set; }
        public CountryListDTO? Country { get; set; }
        public List<DistrictListDTO> Districts { get; set; } = new List<DistrictListDTO>();
    }
}
