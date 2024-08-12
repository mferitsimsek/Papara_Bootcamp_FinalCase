using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.CouponCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Services.CouponServices;
using Papara.CaptainStore.Application.Tools;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.CouponDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.CouponHandlers
{
    public class CouponCreateCommandHandler : IRequestHandler<CouponCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Coupon> _validator;
        private readonly ISessionContext _sessionContext;
        private readonly ICouponService _couponService;
        public CouponCreateCommandHandler(IMapper mapper, IValidator<Coupon> validator, ISessionContext sessionContext, ICouponService couponService)
        {
            _mapper = mapper;
            _validator = validator;
            _sessionContext = sessionContext;
            _couponService = couponService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(CouponCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {

                if (string.IsNullOrEmpty(request.CouponCode))
                    request.CouponCode = GenerateRandomCode.GenerateRandomCouponCode(request.CouponStartChars);

                var existingCoupon = await _couponService.CheckCouponIsExist(request.CouponCode);
                if (existingCoupon != null) return existingCoupon;
                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapForCreateAsync(
                            request,
                            _mapper,
                            _validator,
                            () => Task.FromResult<Coupon>(new Coupon())
                        );

                if (result.status != 200)
                {
                    return result;
                }

                await _couponService.SaveCoupon(result.data as Coupon);

                return new ApiResponseDTO<object?>(201, _mapper.Map<CouponListDTO>(result.data), new List<string> { "Kupon oluşturma işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kupon oluşturma işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
