using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Domain.DTOs.OrderDTOs
{
    public class OrderDetailDTO
    {
        public int ProductId { get; set; }
        [JsonIgnore]
        public string? ProductName { get; set; }
        [JsonIgnore]
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}
