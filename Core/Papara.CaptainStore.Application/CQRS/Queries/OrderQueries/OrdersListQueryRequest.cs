using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.OrderQueries
{
    public class OrdersListQueryRequest : IRequest<ApiResponseDTO<List<OrderListDTO>?>>
    {
    }
}
