using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;

namespace Papara.CaptainStore.Domain.DTOs.ProductDTOs
{
    public class ProductListDTO
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int PointsPercentage { get; set; }
        public int MaxPoints { get; set; }
        public ICollection<CategoryListDTO> Categories { get; set; }
    }
}
