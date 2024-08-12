using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.Services.ProductServices
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponseDTO<object?>?> CheckProductIsExist(string productName)
        {
            return await CheckEntityExists(c => c.ProductName == productName, "Eklemek istediğiniz ürün sistemde kayıtlıdır.");
        }
        public async Task<ApiResponseDTO<object?>> SaveProduct(Product product)
        {
            return await SaveEntity(product);
        }
        public async Task<ApiResponseDTO<object?>> UpdateProduct(Product product)
        {
            return await UpdateEntity(product);
        }
        public async Task<ICollection<Category>> GetCategoriesByIdsAsync(IEnumerable<int> categoryIds)
        {
            var categories = new List<Category>();

            foreach (var categoryId in categoryIds)
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryId);
                if (category != null)
                {
                    categories.Add(category);
                }

            }

            if (categories.Count != categoryIds.Count())
            {
                throw new Exception("Bir veya daha fazla kategori bulunamadı.");
            }

            return categories;
        }


    }
}
