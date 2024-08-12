using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.ProductCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.ProductHandlers
{
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductDeleteCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<object?>> Handle(ProductDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
                if (product == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek ürün bulunamadı." });
                }

                await _unitOfWork.ProductRepository.DeleteAsync(product);
                await _unitOfWork.Complete();

                return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
