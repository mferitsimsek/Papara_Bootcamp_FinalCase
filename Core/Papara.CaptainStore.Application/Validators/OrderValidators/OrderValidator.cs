using FluentValidation;
using Papara.CaptainStore.Application.CQRS.Commands.OrderCommands;
using Papara.CaptainStore.Domain.Entities.OrderEntities;

namespace Papara.CaptainStore.Application.Validators.OrderValidators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.BasketTotal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Sepet toplamı 0'dan büyük veya eşit olmalıdır.");

            RuleFor(order => order.CouponDiscountAmount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Kupon toplamı 0'dan büyük veya eşit olmalıdır.");

            RuleFor(order => order.PointsTotal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Puan toplamı 0'dan büyük veya eşit olmalıdır.");

            RuleFor(order => order.OrderNumber)
                .NotEmpty()
                .WithMessage("Sipariş numarası boş bırakılamaz.");

            RuleFor(order => order.OrderDetails)
                .NotEmpty()
                .WithMessage("Sipariş detayları boş bırakılamaz.");

            RuleForEach(order => order.OrderDetails)
                .ChildRules(details =>
                {
                    details.RuleFor(d => d.Quantity)
                        .GreaterThan(0)
                        .WithMessage("Sipariş edilecek ürün miktarı 0'dan büyük olmalıdır.");
                });
        }
    }
}
