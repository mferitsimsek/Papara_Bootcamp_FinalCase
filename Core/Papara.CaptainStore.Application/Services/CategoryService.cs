using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Application.Services
{
    public class CategoryService : BaseService<Category>
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<ApiResponseDTO<object?>?> CheckCategoryIsExist(string categoryName)
        {
            return await CheckEntityExists(c => c.CategoryName == categoryName, "Eklemek istediğiniz kategori sistemde kayıtlıdır.");
        }
        public async Task<ApiResponseDTO<object?>> SaveCategory(Category category)
        {
            return await SaveEntity(category);
        }
        public async Task<ApiResponseDTO<object?>> UpdateCategory(Category category)
        {
            return await UpdateEntity(category);
        }

    }

}
