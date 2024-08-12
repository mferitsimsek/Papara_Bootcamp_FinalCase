using AutoMapper;
using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs.OrderDTOs;
using Papara.CaptainStore.Domain.Entities.CouponEntities;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;
using Papara.CaptainStore.Domain.Entities.OrderEntities;
using Papara.CaptainStore.Domain.Enums;

namespace Papara.CaptainStore.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public Order CreateOrder(OrderCreateCommandRequest request, string orderNumber, decimal basketTotal, decimal couponDiscountAmount, decimal usedPointsTotal,decimal paidAmount)
        {
            var order = _mapper.Map<Order>(request);

            if (paidAmount <= 0)
                order.PaymentCompleted = true;

            order.BasketTotal = basketTotal;
            order.CouponDiscountAmount = couponDiscountAmount;
            order.PointsTotal = usedPointsTotal;
            order.OrderNumber = orderNumber;
            //order.CreatedUserId = request.CreatedUserId;

            return order;
        }

        public string GenerateOrderNumber()
        {
            return new Random().Next(100000000, 999999999).ToString();
        }

        public async Task<OrderBasketDetailsResultDTO> GetBasketDetails(List<OrderDetailDTO> orderDetails)
        {
            decimal basketTotal = 0;
            decimal pointsPercentageTotal = 0;
            decimal maxEarnPoints = 0;
            foreach (var detail in orderDetails)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    return new OrderBasketDetailsResultDTO
                    {
                        IsSuccess = false,
                        Errors = new List<string> { "Ürün bulunamadı." }
                    };
                }

                detail.ProductName = product.ProductName;
                detail.Price = product.Price;
                basketTotal += detail.Quantity * product.Price;
                maxEarnPoints += product.MaxPoints * detail.Quantity;
                pointsPercentageTotal += product.PointsPercentage * detail.Quantity;
            }
            return new OrderBasketDetailsResultDTO
            {
                IsSuccess = true,
                BasketTotal = basketTotal,
                PointsPercentageTotal = pointsPercentageTotal,
                MaxEarnedPoints = maxEarnPoints
            };
        }

        public async Task SaveOrder(Order order)
        {
            await _unitOfWork.OrderRepository.CreateAsync(order);
            await _unitOfWork.Complete();
        }
        public async Task UpdateOrderPaymentStatusAsync(int orderId, bool paymentCompleted)
        {
            var order = await _unitOfWork.OrderRepository.GetByFilterAsync(o => o.Id == orderId);

            if (order == null)
            {
                throw new ArgumentException("Geçersiz sipariş ID'si");
            }

            order.PaymentCompleted = paymentCompleted;
            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.OrderRepository.SaveChangesAsync();
        }

        public async Task UpdateUserAndCoupon(OrderFinalAmountsDTO finalAmounts, Coupon coupon, CustomerAccount userAccount, OrderBasketDetailsResultDTO basketDetailsResultDTO)
        {
            if (coupon != null)
            {
                coupon.UsedCount++;
                await _unitOfWork.CouponRepository.UpdateAsync(coupon);
            }

            decimal earnedPoints = finalAmounts.PaidAmount * (basketDetailsResultDTO.PointsPercentageTotal / 100);
            if (earnedPoints > basketDetailsResultDTO.MaxEarnedPoints) // Puan kazandırma yüzdesinden elde edilen kazanç ürünlerin maksimum puan toplamından büyük ise kullanıcıya maksimum puanı yüklüyoruz.
                earnedPoints = basketDetailsResultDTO.MaxEarnedPoints;

            userAccount.Points = Math.Max(0, userAccount.Points - finalAmounts.UsedPointsTotal) + earnedPoints;

            await _unitOfWork.CustomerAccountRepository.UpdateAsync(userAccount);
        }

        public async Task<OrderCouponDetailsResultDTO> ValidateCouponCode(string couponCode)
        {
            if (string.IsNullOrWhiteSpace(couponCode))
                return new OrderCouponDetailsResultDTO { IsSuccess = true };

            var coupon = await _unitOfWork.CouponRepository.GetByFilterAsync(x => x.CouponCode == couponCode);
            if (coupon == null || coupon.UsedCount >= coupon.MaxUsageCount || coupon.ValidFrom > DateTime.Now || coupon.ValidTo < DateTime.Now)
            {
                return new OrderCouponDetailsResultDTO
                {
                    IsSuccess = false,
                    Errors = new List<string> { "Geçersiz kupon kodu." }
                };
            }

            return new OrderCouponDetailsResultDTO
            {
                IsSuccess = true,
                Coupon = coupon,
                CouponDiscountAmount = coupon.DiscountAmount
            };
        }

        public OrderFinalAmountsDTO CalculateFinalAmounts(decimal basketTotal, decimal couponDiscountAmount, DiscountType couponDiscountType, decimal pointsTotal)
        {
            decimal finalTotal, paidAmount, usedPointsTotal = 0;
            if (couponDiscountType == DiscountType.Percentage)
            {
                // discountType yüzde cinsinden bir indirim olduğunu belirtir
                // couponDiscountAmount, yüzde cinsinden indirim oranını temsil ediyor (örneğin 25)
                decimal discountRate = couponDiscountAmount / 100; // Yüzdelik oranı hesapla
                finalTotal = basketTotal * (1 - discountRate);
            }
            if (basketTotal < couponDiscountAmount)
            {
                // eğer kupon sepet tutarından fazla ise ödeme yapılmadan sipariş tamamlanır ve puan kazanımı olmaz.
                return new OrderFinalAmountsDTO
                {
                    PaidAmount = 0,
                    UsedPointsTotal = 0
                };
            }
           
            finalTotal = basketTotal - couponDiscountAmount;
                
            if (finalTotal > pointsTotal)
            {
                paidAmount = finalTotal - pointsTotal;
                usedPointsTotal = pointsTotal; // bir üst satırda tüm puanlar kullanıldığı için kullanılan puan miktarına yazıyoruz.
            }
            else
            {
                usedPointsTotal = finalTotal; //puanlar finalTotal den büyük ise Puanlardan finalTotal miktarı kadar kullanılmış olacak.
                paidAmount = 0;
            }

            return new OrderFinalAmountsDTO
            {
                PaidAmount = paidAmount,
                UsedPointsTotal = usedPointsTotal
            };
        }

        public async Task<decimal> CalculatePaymentAmountAsync(int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetByFilterAsync(o => o.Id == orderId);
            if (order == null)
            {
                throw new ArgumentException("Geçersiz sipariş ID'si");
            }
            var total = order.BasketTotal - order.CouponDiscountAmount - order.PointsTotal;
            return total < 0 ||order.PaymentCompleted==true ? 0 : total;
        }
    }
}
