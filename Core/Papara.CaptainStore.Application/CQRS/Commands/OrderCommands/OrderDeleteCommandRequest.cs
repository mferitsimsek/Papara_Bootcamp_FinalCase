using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.OrderCommands
{
    public class OrderDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int OrderId { get; set; }
    }
}
