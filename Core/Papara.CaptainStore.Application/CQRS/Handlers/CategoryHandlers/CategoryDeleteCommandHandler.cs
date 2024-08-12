using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.Caching;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CategoryHandlers
{
    public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryService _categoryService;
        private readonly ICacheService _cacheService;

        public CategoryDeleteCommandHandler(IUnitOfWork unitOfWork, CategoryService categoryService, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        public async Task<ApiResponseDTO<object?>> Handle(CategoryDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
                if (category == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek kategori bulunamadı." });
                }
                category.IsDeleted = true;
                await _categoryService.UpdateEntity(category);
                await _cacheService.RemoveAsync("Categories");

                return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
