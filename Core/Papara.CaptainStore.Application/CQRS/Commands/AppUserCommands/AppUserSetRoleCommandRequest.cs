using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands
{
    public class AppUserSetRoleCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public Guid AppUserId { get; set; }
        public Guid AppRoleId { get; set; }
        public required string RoleName { get; set; }
    }
}
