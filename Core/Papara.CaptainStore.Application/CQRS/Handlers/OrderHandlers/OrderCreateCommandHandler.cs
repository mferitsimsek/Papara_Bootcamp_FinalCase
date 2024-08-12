using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Application.Services.OrderServices;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.OrderEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.OrderHandlers
{
    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISessionContext _sessionContext;
        private readonly IValidator<Order> _validator;
        private readonly IOrderService _orderService;        

        public OrderCreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork, ISessionContext sessionContext, IValidator<Order> validator, IOrderService orderService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sessionContext = sessionContext;
            _validator = validator;
            _orderService = orderService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(OrderCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var setUserAndDateResponse = RequestHelper.SetUserAndDate(request, _sessionContext);
                if (setUserAndDateResponse != null) return setUserAndDateResponse;

                var result = await ValidationHelper.ValidateAndMapForCreateAsync(
                            request,
                            _mapper,
                            _validator,
                            () => Task.FromResult<Order>(new Order())
                        );

                string orderNumber = _orderService.GenerateOrderNumber();

                var basketDetails = await _orderService.GetBasketDetails(request.OrderDetails);
                if (!basketDetails.IsSuccess)
                    return new ApiResponseDTO<object?>(404, null, basketDetails.Errors);

                var couponDetails = await _orderService.ValidateCouponCode(request.CouponCode);
                if (!couponDetails.IsSuccess)
                    return new ApiResponseDTO<object?>(400, null, couponDetails.Errors);

                var userAccount = await _unitOfWork.CustomerAccountRepository.GetByFilterAsync(x => x.AppUserId == request.CreatedUserId);
                if (userAccount == null) return new ApiResponseDTO<object?>(404, null, new List<string> { "Kullanıcı hesabı bulunamadı." });

                var finalAmounts = _orderService.CalculateFinalAmounts(basketDetails.BasketTotal, couponDetails.CouponDiscountAmount, couponDetails.DiscountType, userAccount.Points);
                await _orderService.UpdateUserAndCoupon(finalAmounts, couponDetails.Coupon, userAccount, basketDetails);

                var order = _orderService.CreateOrder(request, orderNumber, basketDetails.BasketTotal, couponDetails.CouponDiscountAmount, finalAmounts.UsedPointsTotal, finalAmounts.PaidAmount);
                await _orderService.SaveOrder(order);
                
                await _orderService.SendOrderReceivedEmailAsync(order);

                return new ApiResponseDTO<object?>(201, order, new List<string> { "Sipariş başarıyla oluşturuldu." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Sipariş oluşturma sırasında bir hata oluştu.", ex.Message });
            }
        }
    }
}
