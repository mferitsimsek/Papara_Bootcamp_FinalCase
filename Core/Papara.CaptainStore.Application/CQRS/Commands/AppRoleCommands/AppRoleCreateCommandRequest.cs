using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands
{
    public class AppRoleCreateCommandRequest : IRequest<ApiResponseDTO<object?>>
    {
        public required string Name { get; set; }
       
    }
}
