using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Domain.Entities.ProductEntities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Features { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int PointsPercentage { get; set; }
        public int MaxPoints { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
