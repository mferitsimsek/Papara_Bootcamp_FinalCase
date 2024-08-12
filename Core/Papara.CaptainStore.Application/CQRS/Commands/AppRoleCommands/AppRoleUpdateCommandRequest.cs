using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands
{
    public class AppRoleUpdateCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public Guid AppRoleId { get; set; }
        public required string Name { get; set; }
    }
}
