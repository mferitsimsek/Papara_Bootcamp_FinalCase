using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.AppUserDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.AppUserQueries
{
    public class AppUserListQueryRequest : IRequest<ApiResponseDTO<List<AppUserListDTO>?>>
    {
    }
}
