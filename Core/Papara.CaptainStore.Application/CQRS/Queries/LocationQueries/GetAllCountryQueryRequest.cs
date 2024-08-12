using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.LocationQueries
{
    public class GetAllCountryQueryRequest : IRequest<ApiResponseDTO<List<CountryListDTO>?>>
    {
    }
}
