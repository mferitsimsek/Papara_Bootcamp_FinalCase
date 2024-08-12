using MediatR;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CategoryDTOs;

namespace Papara.CaptainStore.Application.CQRS.Queries.CategoryQueries
{
    public class CategoryListQueryRequest : IRequest<ApiResponseDTO<List<CategoryListDTO>?>>
    {
    }
}
