using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands
{
    public class AppUserDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public Guid AppUserId { get; set; }
    }
}
