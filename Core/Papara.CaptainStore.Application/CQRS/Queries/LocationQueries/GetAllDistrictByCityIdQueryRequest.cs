using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.LocationQueries
{
    public class GetAllDistrictByCityIdQueryRequest : IRequest<ApiResponseDTO<List<DistrictListDTO>?>>
    {
        public int CityId { get; set; }
    }
}
