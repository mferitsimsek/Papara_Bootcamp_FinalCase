using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CategoryHandlers
{
    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Category> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly CategoryService _categoryService;
        private readonly ICacheService _cacheService;

        public CategoryUpdateCommandHandler(IMapper mapper, IValidator<Category> validator, IUnitOfWork unitOfWork, ISessionContext sessionContext, CategoryService categoryService, ICacheService cacheService)
        {
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _sessionContext = sessionContext;
            _categoryService = categoryService;
            _cacheService = cacheService;
        }

        public async Task<ApiResponseDTO<object?>> Handle(CategoryUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapAsync(
                            request,
                            _mapper,
                            _validator,
                            () => _unitOfWork.CategoryRepository.GetByFilterAsync(p => p.CategoryId == request.CategoryId));

                if (result.status != 200)
                {
                    return result;
                }

                await _categoryService.UpdateEntity(result.data as Category);
                await _cacheService.RemoveAsync("Categories");

                return new ApiResponseDTO<object?>(201, _mapper.Map<CategoryListDTO>(result.data), new List<string> { "Güncelleme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata işleme
                //return HandleException(ex);
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
            }

        }
        //private IDTO<object?> HandleException(Exception ex)
        //{
        //    // Exception logging veya daha ileri işlem yapılabilir
        //    return new IDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu." });
        //}
    }
}
