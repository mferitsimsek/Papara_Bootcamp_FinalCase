using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.CustomerAccountCommands
{
    public class CustomerAccountPointsUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public int CustomerAccountId { get; set; }
        public decimal Points { get; set; }
    }
}
