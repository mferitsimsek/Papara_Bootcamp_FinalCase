using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.Services.ElasticSearchProductService
{
    public interface IElasticsearchProductService
    {
        Task<string> IndexAllProductsAsync();
        Task<List<ProductListDTO>> GetAllProductsAsync(int from = 0, int size = 100);
        Task<List<ProductListDTO>> GetProductsByFieldNameAsync(string fieldName, string fieldValue, int from = 0, int size = 10);
    }
}
