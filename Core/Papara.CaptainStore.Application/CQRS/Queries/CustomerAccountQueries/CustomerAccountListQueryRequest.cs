using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.CustomerAccountQueries
{
    public class CustomerAccountListQueryRequest : IRequest<ApiResponseDTO<List<CustomerAccountListDTO>?>>
    {
    }
}
