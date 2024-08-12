using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.ProductQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, ApiResponseDTO<ProductListDTO?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDTO<ProductListDTO?>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
                if (coupon == null)
                {
                    return new ApiResponseDTO<ProductListDTO?>(404, null, new List<string> { "Ürün bulunamadı." });
                }

                var couponDto = _mapper.Map<ProductListDTO>(coupon);
                return new ApiResponseDTO<ProductListDTO?>(200, couponDto, new List<string> { "Sorgulama işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<ProductListDTO?>(500, null, new List<string> { "Sorgulama işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
