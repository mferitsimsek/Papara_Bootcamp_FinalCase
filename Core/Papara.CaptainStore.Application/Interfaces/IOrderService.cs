using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;
using Papara.CaptainStore.Domain.Entities.OrderEntities;
using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IOrderService
    {
        Task UpdateUserAndCoupon(OrderFinalAmountsDTO finalAmounts, Coupon coupon, CustomerAccount userAccount, OrderBasketDetailsResultDTO basketDetailsResultDTO);
        Order CreateOrder(OrderCreateCommandRequest request, string orderNumber, decimal basketTotal, decimal couponDiscountAmount, decimal usedPointsTotal,decimal paidAmount);
        OrderFinalAmountsDTO CalculateFinalAmounts(decimal basketTotal, decimal couponDiscountAmount, DiscountType couponDiscountType, decimal pointsTotal);
        Task<OrderBasketDetailsResultDTO> GetBasketDetails(List<OrderDetailDTO> orderDetails);
        Task<OrderCouponDetailsResultDTO> ValidateCouponCode(string couponCode);
        Task<decimal> CalculatePaymentAmountAsync(int orderId);
        string GenerateOrderNumber();
        Task SaveOrder(Order order);
        Task UpdateOrderPaymentStatusAsync(int orderId, bool paymentCompleted);
    }
}
