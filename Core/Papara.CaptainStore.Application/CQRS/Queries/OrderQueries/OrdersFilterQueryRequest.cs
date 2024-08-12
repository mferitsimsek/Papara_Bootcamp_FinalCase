using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.OrderQueries
{
    public class OrdersFilterQueryRequest : IRequest<ApiResponseDTO<List<OrderListDTO>?>>
    {
        public bool PaymentCompleted { get; set; }
    }
}
