using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.ProductQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.ProductEntities;
using System.Linq.Expressions;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class GetProductsByCategoryIdQueryHandler : IRequestHandler<GetProductsByCategoryIdQueryRequest, ApiResponseDTO<List<ProductListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductsByCategoryIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<List<ProductListDTO>?>> Handle(GetProductsByCategoryIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _unitOfWork.ProductRepository.GetAllByFilterAsync(
                   product => product.Categories.Any(c => c.CategoryId == request.CategoryId), // Filtre
                   new List<Expression<Func<Product, object>>> { p => p.Categories } // Include edilen Categories
               );

                if (products.Any())
                {
                    var dtoProducts = _mapper.Map<List<ProductListDTO>>(products);
                    return new ApiResponseDTO<List<ProductListDTO>?>(200, dtoProducts, new List<string> { "Ürünler başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<ProductListDTO>?>(404, null, new List<string> { "Kategoriye ait herhangi bir ürün bulunamadı." });
            }
            catch (Exception ex)
            {
                // Hata loglama işlemleri yapılabilir
                return new ApiResponseDTO<List<ProductListDTO>?>(500, null, new List<string> { "Ürün listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
