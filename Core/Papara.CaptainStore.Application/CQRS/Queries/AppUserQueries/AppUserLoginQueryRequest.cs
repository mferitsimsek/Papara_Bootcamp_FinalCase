using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.AppUserQueries
{
    public class AppUserLoginQueryRequest : IRequest<ApiResponseDTO<AppUserLoginDTO?>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
