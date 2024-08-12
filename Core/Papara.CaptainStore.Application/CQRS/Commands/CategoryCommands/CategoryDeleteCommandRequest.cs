using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CategoryCommands
{
    public class CategoryDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int CategoryId { get; set; }
    }
}
