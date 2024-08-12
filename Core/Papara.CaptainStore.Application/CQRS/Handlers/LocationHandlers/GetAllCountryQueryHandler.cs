using AutoMapper;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Papara.CaptainStore.Application.CQRS.Queries.LocationQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.Caching;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.LocationEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.LocationHandlers
{
    public class GetAllCountryQueryHandler : IRequestHandler<GetAllCountryQueryRequest, ApiResponseDTO<List<CountryListDTO>?>>
    {
        private readonly IRepository<Country> _repository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;
        public GetAllCountryQueryHandler(IRepository<Country> repository, IMapper mapper, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ApiResponseDTO<List<CountryListDTO>?>> Handle(GetAllCountryQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cacheKey = "Countries";
                var cachedData = await _cacheService.GetAsync<List<CountryListDTO>>(cacheKey);
                if (cachedData != null)
                    return new ApiResponseDTO<List<CountryListDTO>?>(200, cachedData, new List<string> { "Ülke listesi başarılı bir şekilde getirildi. -FromCache" });
                var datas = await _repository.GetAllAsync([x => x.Cities]);
                if (!datas.IsNullOrEmpty())
                {
                    var countryListDTOs = _mapper.Map<List<CountryListDTO>>(datas);
                    await _cacheService.SetAsync(cacheKey, countryListDTOs, TimeSpan.FromDays(1), TimeSpan.FromHours(1));

                    return new ApiResponseDTO<List<CountryListDTO>?>(200, countryListDTOs, ["Ülke listesi başarılı bir şekilde getirildi."]);
                }
                else
                {
                    return new ApiResponseDTO<List<CountryListDTO>?>(404, null, ["Herhangi bir ülke bulunamadı."]);
                }
            }
            catch (Exception)
            {
                return new ApiResponseDTO<List<CountryListDTO>?>(500, null, ["Ülke listesi getirilirken bir sorun oluştu."]);
            }
        }
    }
}
