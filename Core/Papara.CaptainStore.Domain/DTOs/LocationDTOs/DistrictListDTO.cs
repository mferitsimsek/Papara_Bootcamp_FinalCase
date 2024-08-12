namespace Papara.CaptainStore.Domain.DTOs
{
    public class DistrictListDTO
    {
        public int DistrictId { get; set; }
        public required string Definition { get; set; }
        public int CityId { get; set; }
        public CityListDTO? City { get; set; }
    }
}
