using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands
{
    public class AppUserPasswordResetCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
    }
}
