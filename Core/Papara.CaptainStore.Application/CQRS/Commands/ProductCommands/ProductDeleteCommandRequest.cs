using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.ProductCommands
{
    public class ProductDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int ProductId { get; set; }
    }
}
