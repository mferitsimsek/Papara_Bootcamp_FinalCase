using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.CategoryQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.Caching;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CategoryHandlers
{
    public class CategoryListQueryHandler : IRequestHandler<CategoryListQueryRequest, ApiResponseDTO<List<CategoryListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public CategoryListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<ApiResponseDTO<List<CategoryListDTO>?>> Handle(CategoryListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = "Categories";
                if (_cacheService.IsConnected())
                {
                    var cachedData = await _cacheService.GetAsync<List<CategoryListDTO>>(cacheKey);
                    if (cachedData != null)
                        return new ApiResponseDTO<List<CategoryListDTO>?>(200, cachedData, new List<string> { "Kategoriler başarıyla getirildi. -FromCache" });
                }


                var categorys = await _unitOfWork.CategoryRepository.GetAllAsync();

                if (categorys.Any())
                {
                    var dtoCategorys = _mapper.Map<List<CategoryListDTO>>(categorys);
                    if (_cacheService.IsConnected())
                        await _cacheService.SetAsync(cacheKey, dtoCategorys, TimeSpan.FromDays(1), TimeSpan.FromHours(1));


                    return new ApiResponseDTO<List<CategoryListDTO>?>(200, dtoCategorys, new List<string> { "Kategoriler başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<CategoryListDTO>?>(404, null, new List<string> { "Herhangi bir kategori bulunamadı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<CategoryListDTO>?>(500, null, new List<string> { "Kategori listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}


