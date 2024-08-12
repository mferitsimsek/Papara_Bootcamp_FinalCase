using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.CouponQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class CouponsListQueryHandler : IRequestHandler<CouponsListQueryRequest, ApiResponseDTO<List<CouponListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponsListQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<List<CouponListDTO>?>> Handle(CouponsListQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var coupons = await _unitOfWork.CouponRepository.GetAllAsync();

                if (coupons.Any())
                {
                    var couponsDto = _mapper.Map<List<CouponListDTO>>(coupons);
                    return new ApiResponseDTO<List<CouponListDTO>?>(200, couponsDto, new List<string> { "Kuponlar başarıyla getirildi." });
                }

                return new ApiResponseDTO<List<CouponListDTO>?>(404, null, new List<string> { "Herhangi bir kupon bulunamadı." });
            }
            catch (Exception ex)
            {
                // Hata loglama işlemleri yapılabilir
                return new ApiResponseDTO<List<CouponListDTO>?>(500, null, new List<string> { "Kupon listesi getirilirken bir hata oluştu.", ex.Message });
            }
        }
    }
}
