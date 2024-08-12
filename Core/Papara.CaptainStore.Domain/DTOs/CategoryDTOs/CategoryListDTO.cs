using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using System.Text.Json.Serialization;

namespace Papara.CaptainStore.Domain.DTOs.CategoryDTOs
{
    public class CategoryListDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        [JsonIgnore]
        public ICollection<ProductListDTO> Products { get; set; }
    }
}
