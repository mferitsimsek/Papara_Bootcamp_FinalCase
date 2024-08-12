using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Papara.CaptainStore.Application.CQRS.Queries.LocationQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.LocationHandlers
{
    public class GetAllCityByCountryIdQueryHandler : IRequestHandler<GetAllCityByCountryIdQueryRequest, ApiResponseDTO<List<CityListDTO>?>>
    {
        private readonly IRepository<City> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetAllCityByCountryIdQueryHandler(IRepository<City> repository, IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ApiResponseDTO<List<CityListDTO>?>> Handle(GetAllCityByCountryIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = $"CitiesByCountryId_{request.CountryId}";
                var cachedData = await _cacheService.GetAsync<List<CityListDTO>>(cacheKey);
                if (cachedData != null)
                    return new ApiResponseDTO<List<CityListDTO>?>(200, cachedData, new List<string> { "Şehir listesi başarılı bir şekilde getirildi. -FromCache" });

                var datas = await _repository.GetAllByFilterAsync(x => x.CountryId == request.CountryId);
                if (!datas.IsNullOrEmpty())
                {
                    var cityListDTOs = _mapper.Map<List<CityListDTO>>(datas);
                    await _cacheService.SetAsync(cacheKey, cityListDTOs, TimeSpan.FromDays(1), TimeSpan.FromHours(1));

                    return new ApiResponseDTO<List<CityListDTO>?>(200, cityListDTOs, ["Şehir listesi başarılı bir şekilde getirildi. "]);
                }
                else
                {
                    return new ApiResponseDTO<List<CityListDTO>?>(404, null, ["Herhangi bir şehir bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new ApiResponseDTO<List<CityListDTO>?>(500, null, ["Şehir listesi getirilirken bir sorun oluştu."]);
            }
        }
    }
}
