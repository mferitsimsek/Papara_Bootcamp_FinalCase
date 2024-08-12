using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CustomerAccountDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.CustomerAccountQueries
{
    public class CustomerAccountByIdQueryRequest : IRequest<ApiResponseDTO<CustomerAccountListDTO?>>
    {
        public int CustomerAccountId { get; set; }
    }
}
