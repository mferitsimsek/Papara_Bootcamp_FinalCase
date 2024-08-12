using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.LocationQueries
{
    public class GetAllCountryQueryRequest : IRequest<ApiResponseDTO<List<CountryListDTO>?>>
    {
        public int PageNumber { get; set; } = 1; // Varsayılan sayfa numarası
        public int PageSize { get; set; } = 10; // Varsayılan sayfa boyutu
    }
}
