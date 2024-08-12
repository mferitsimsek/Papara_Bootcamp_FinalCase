using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces.Caching;
using Papara.CaptainStore.Application.Services.CategoryServices;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CategoryHandlers
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Category> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly ICategoryService _categoryService;
        private readonly ICacheService _cacheService;

        public CategoryCreateCommandHandler(IMapper mapper, IValidator<Category> validator, ISessionContext sessionContext, ICategoryService categoryService, ICacheService cacheService)
        {
            _mapper = mapper;
            _validator = validator;
            _sessionContext = sessionContext;
            _categoryService = categoryService;
            _cacheService = cacheService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(CategoryCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var existingCategory = await _categoryService.CheckCategoryIsExist(request.CategoryName);
                if (existingCategory != null) return existingCategory;


                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapForCreateAsync(
                            request,
                            _mapper,
                            _validator,
                            () => Task.FromResult<Category>(new Category())
                        );

                if (result.status != 200)
                {
                    return result;
                }

                await _categoryService.SaveCategory(result.data as Category);
                await _cacheService.RemoveAsync("Categories");
                return new ApiResponseDTO<object?>(201, _mapper.Map<CategoryListDTO>(result.data), new List<string> { "Kayıt işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
