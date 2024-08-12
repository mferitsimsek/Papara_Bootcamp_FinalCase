using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands
{
    public class AppRoleDeleteCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public Guid AppRoleId { get; set; }
    }

}