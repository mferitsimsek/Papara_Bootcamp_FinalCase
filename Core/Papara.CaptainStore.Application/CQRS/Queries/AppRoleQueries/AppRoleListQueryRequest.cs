using MediatR;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.AppRoleQueries
{
    public class AppRoleListQueryRequest : IRequest<ApiResponseDTO<List<AppRoleListDTO>?>>
    {
    }
}
