using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.ProductCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services.ProductServices;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.ProductDTOs;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Product> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly IProductService _productService;


        public ProductUpdateCommandHandler(IMapper mapper, IValidator<Product> validator, IUnitOfWork unitOfWork, ISessionContext sessionContext, IProductService productService)
        {
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _sessionContext = sessionContext;
            _productService = productService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(ProductUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapAsync(
                            request,
                            _mapper,
                            _validator,
                            () => _unitOfWork.ProductRepository.GetByFilterAsync(p => p.ProductId == request.ProductId));

                if (result.status != 200)
                {
                    return result;
                }

                await _productService.UpdateProduct(result.data as Product);

                return new ApiResponseDTO<object?>(201, _mapper.Map<ProductListDTO>(result.data), new List<string> { "Ürün Güncelleme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Ürün Güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
