using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.ProductQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class ProductListQueryHandler : IRequestHandler<ProductListQueryRequest, ApiResponseDTO<PagedResult<ProductListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<PagedResult<ProductListDTO>?>> Handle(ProductListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllAsync(request.PageNumber, request.PageSize);

                if (!products.Items.Any())
                    return new ApiResponseDTO<PagedResult<ProductListDTO>?>(404, null, new List<string> { "Herhangi bir ürün bulunamadı." });


                var dtoProducts = _mapper.Map<List<ProductListDTO>>(products.Items);
                var pagedResult = new PagedResult<ProductListDTO>(dtoProducts, products.TotalCount, request.PageNumber, request.PageSize);
                return new ApiResponseDTO<PagedResult<ProductListDTO>?>(200, pagedResult, new List<string> { "Ürünler başarıyla getirildi." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<PagedResult<ProductListDTO>?>(500, null, new List<string> { "Ürün listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
