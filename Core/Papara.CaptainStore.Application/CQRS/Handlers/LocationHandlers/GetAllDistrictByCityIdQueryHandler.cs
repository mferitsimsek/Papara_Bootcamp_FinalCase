using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Papara.CaptainStore.Application.CQRS.Queries.LocationQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.CachingService;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.LocationHandlers
{
    public class GetAllDistrictByCityIdQueryHandler : IRequestHandler<GetAllDistrictByCityIdQueryRequest, ApiResponseDTO<List<DistrictListDTO>?>>
    {
        private readonly IRepository<District> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GetAllDistrictByCityIdQueryHandler(IRepository<District> repository, IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ApiResponseDTO<List<DistrictListDTO>?>> Handle(GetAllDistrictByCityIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = $"DistrictsByCityId_{request.CityId}";
                var cachedData = await _cacheService.GetAsync<List<DistrictListDTO>>(cacheKey);
                if (cachedData != null)
                    return new ApiResponseDTO<List<DistrictListDTO>?>(200, cachedData, new List<string> { "İlçe listesi başarılı bir şekilde getirildi. -FromCache" });
                var datas = await _repository.GetAllByFilterAsync(x => x.CityId == request.CityId);
                if (!datas.IsNullOrEmpty())
                {
                    var districtListDTOs = _mapper.Map<List<DistrictListDTO>>(datas);
                    await _cacheService.SetAsync(cacheKey, districtListDTOs, TimeSpan.FromDays(1), TimeSpan.FromHours(1));

                    return new ApiResponseDTO<List<DistrictListDTO>?>(200, districtListDTOs, ["İlçe listesi başarılı bir şekilde getirildi"]);
                }
                else
                {
                    return new ApiResponseDTO<List<DistrictListDTO>?>(404, null, ["Herhangi bir ilçe bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new ApiResponseDTO<List<DistrictListDTO>?>(500, null, ["İlçe listesi getirilirken bir sorun oluştu."]);
            }
        }
    }
}
