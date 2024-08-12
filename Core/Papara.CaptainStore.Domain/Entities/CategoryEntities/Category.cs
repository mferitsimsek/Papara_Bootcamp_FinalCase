using Papara.CaptainStore.Domain.Entities.BaseEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Domain.Entities.CategoryEntities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Url { get; set; }
        public string Tag { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUserId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
