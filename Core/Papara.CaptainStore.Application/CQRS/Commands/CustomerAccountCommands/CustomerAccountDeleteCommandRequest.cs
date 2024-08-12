using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands
{
    public class CustomerAccountDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int CustomerAccountId { get; set; }
    }
}
