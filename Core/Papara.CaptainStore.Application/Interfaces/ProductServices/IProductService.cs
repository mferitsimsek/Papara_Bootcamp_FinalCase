using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.Interfaces.ProductServices
{
    public interface IProductService
    {
        Task<ApiResponseDTO<object?>?> CheckProductIsExist(string productName);
        Task<ApiResponseDTO<object?>> SaveProduct(Product product);
        Task<ApiResponseDTO<object?>> UpdateProduct(Product product);
        Task<ICollection<Category>> GetCategoriesByIdsAsync(IEnumerable<int> categoryIds);
    }
}
