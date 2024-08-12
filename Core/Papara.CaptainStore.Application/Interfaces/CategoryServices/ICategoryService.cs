using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Application.Interfaces.CategoryServices
{
    public interface ICategoryService
    {
        Task<ApiResponseDTO<object?>?> CheckCategoryIsExist(string categoryName);
        Task<ApiResponseDTO<object?>> SaveCategory(Category category);
        Task<ApiResponseDTO<object?>> UpdateCategory(Category category);
    }
}
