using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CouponCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Interfaces.CouponServices;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class CouponUpdateCommandHandler : IRequestHandler<CouponUpdateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Coupon> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly ICouponService _couponService;

        public CouponUpdateCommandHandler(IMapper mapper, IValidator<Coupon> validator, IUnitOfWork unitOfWork, ISessionContext sessionContext, ICouponService couponService)
        {
            _mapper = mapper;
            _validator = validator;
            _unitOfWork = unitOfWork;
            _sessionContext = sessionContext;
            _couponService = couponService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(CouponUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapAsync(
                            request,
                            _mapper,
                            _validator,
                            () => _unitOfWork.CouponRepository.GetByFilterAsync(p => p.Id == request.CouponId));

                if (result.status != 200)
                {
                    return result;
                }

                await _couponService.UpdateCoupon(result.data as Coupon);

                return new ApiResponseDTO<object?>(201, _mapper.Map<CouponListDTO>(result.data), new List<string> { "Kupon güncelleme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kupon güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }

    }
}

