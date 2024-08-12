using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.LocationQueries
{
    public class GetAllCityByCountryIdQueryRequest : IRequest<ApiResponseDTO<List<CityListDTO>?>>
    {
        public int CountryId { get; set; }
    }
}
