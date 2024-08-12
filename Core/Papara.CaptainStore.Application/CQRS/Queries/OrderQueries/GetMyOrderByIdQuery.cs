using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.OrderQueries
{
    public class GetMyOrderByIdQuery : IRequest<ApiResponseDTO<OrderListDTO?>>
    {
        public int OrderId { get; set; }
    }
}
