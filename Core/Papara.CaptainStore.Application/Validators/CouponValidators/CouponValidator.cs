using FluentValidation;
using Papara.CaptainStore.Domain.Entities.CouponEntities;

namespace Papara.CaptainStore.Application.Validators.CouponValidators;

public class CouponValidator : AbstractValidator<Coupon>
{
    public CouponValidator()
    {
        RuleFor(coupon => coupon.CouponCode)
            .MaximumLength(10).WithMessage("Kupon kodu 10 haneden büyük olamaz.");
        RuleFor(coupon => coupon.MaxUsageCount)
             .NotEmpty().WithMessage("Maksimum kullanım sayısı boş geçilemez.")
           .GreaterThan(0).WithMessage("Maksimum kullanım adet'i sıfır olamaz.");
        RuleFor(coupon => coupon.DiscountAmount)
            .NotEmpty().WithMessage("İndirim tutarı boş geçilemez.")
            .GreaterThan(0).WithMessage("İndirim tutarı sıfır olamaz.");
        RuleFor(coupon => coupon.ValidFrom)
            .NotEmpty().WithMessage("Kupon Geçerlilik başlangıç tarihi boş geçilemez.")
            .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Geçerlilik başlangıç tarihi bugünden önce olamaz.");
        RuleFor(coupon => coupon.ValidTo)
            .NotEmpty().WithMessage("Kupon Geçerlilik bitiş tarihi boş geçilemez.")
            .LessThanOrEqualTo(new DateTime(DateTime.Now.Year, 12, 31)).WithMessage("Kupon Geçerlilik bitiş tarihi, en fazla bulunduğunuz yıl sonuna kadar olabilir.");
    }
}
