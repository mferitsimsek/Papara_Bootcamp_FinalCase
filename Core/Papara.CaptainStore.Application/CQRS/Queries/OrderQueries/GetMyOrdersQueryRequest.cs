using MediatR;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.OrderQueries
{
    public class GetMyOrdersQueryRequest : IRequest<ApiResponseDTO<List<OrderListDTO>?>>
    {
    }
}
