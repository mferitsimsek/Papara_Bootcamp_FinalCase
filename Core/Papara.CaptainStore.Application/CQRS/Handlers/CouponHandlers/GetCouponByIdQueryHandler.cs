using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.CouponQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQueryRequest, ApiResponseDTO<CouponListDTO?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCouponByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponseDTO<CouponListDTO?>> Handle(GetCouponByIdQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var coupon = await _unitOfWork.CouponRepository.GetByIdAsync(request.CouponId);
                if (coupon == null)
                {
                    return new ApiResponseDTO<CouponListDTO?>(404, null, new List<string> { "Kupon bulunamadı." });
                }

                var couponDto = _mapper.Map<CouponListDTO>(coupon);
                return new ApiResponseDTO<CouponListDTO?>(200, couponDto, new List<string> { "Sorgulama işlemi başarılı." });
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
                return new ApiResponseDTO<CouponListDTO?>(500, null, new List<string> { "Sorgulama işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
