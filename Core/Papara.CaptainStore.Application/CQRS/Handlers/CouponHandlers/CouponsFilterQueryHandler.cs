using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Queries.CouponQueries;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Enums;
using System.Linq.Expressions;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class CouponsFilterQueryHandler : IRequestHandler<CouponsFilterQueryRequest, ApiResponseDTO<List<CouponListDTO>?>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponsFilterQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResponseDTO<List<CouponListDTO>?>> Handle(CouponsFilterQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Expression<Func<Coupon, bool>> filter = c => true; // Başlangıçta tüm kuponları seç

                switch (request.FilterType)
                {
                    case FilterType.Valid:
                        filter = c => c.ValidTo >= DateTime.Now && c.MaxUsageCount > c.UsedCount;
                        break;
                    case FilterType.Used:
                        filter = c => c.UsedCount >= c.MaxUsageCount;
                        break;

                    case FilterType.Expired:
                        filter = c => c.ValidTo < DateTime.Now;
                        break;
                }

                var coupons = await _unitOfWork.CouponRepository.GetAllByFilterAsync(filter);
                if (coupons.Any())
                {
                    var couponListDTOs = _mapper.Map<List<CouponListDTO>>(coupons);

                    return new ApiResponseDTO<List<CouponListDTO>?>(200, couponListDTOs, new List<string> { "Kuponlar başarıyla getirildi." });
                }
                return new ApiResponseDTO<List<CouponListDTO>?>(200, null, new List<string> { "Listelenecek kupon bulunamadı." });

            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<List<CouponListDTO>?>(500, null, new List<string> { "Kuponları getirme sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
