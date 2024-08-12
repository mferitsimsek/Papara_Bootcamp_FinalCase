using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.ProductQueries
{
    public class ProductListQueryRequest : IRequest<ApiResponseDTO<PagedResult<ProductListDTO>?>>
    {
        public int PageNumber { get; set; } = 1; // Varsayılan sayfa numarası
        public int PageSize { get; set; } = 10; // Varsayılan sayfa boyutu
    }

}
