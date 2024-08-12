using FluentValidation;
using Papara.CaptainStore.Domain.Entities.ProductEntities;

namespace Papara.CaptainStore.Application.Validators.ProductValidators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş olamaz.")
                .MaximumLength(255).WithMessage("Ürün adı en fazla 255 karakter olabilir.");

            RuleFor(product => product.Price)
               .NotEmpty().WithMessage("Fiyat bilgisi boş olamaz")
               .GreaterThan(0).WithMessage("Fiyat 0 dan büyük olmalıdır..");

            RuleFor(product => product.Quantity)
               .NotEmpty().WithMessage("Stok bilgisi boş olamaz")
               .GreaterThanOrEqualTo(0).WithMessage("Stok adedi eksi değer olamaz.");

            RuleFor(product => product.Features)
                .NotEmpty().WithMessage("Özellikler boş olamaz.")
                .MaximumLength(1000).WithMessage("Özellikler en fazla 1000 karakter olabilir.");

            RuleFor(product => product.Description)
                .NotEmpty().WithMessage("Açıklama boş olamaz.")
                .MaximumLength(2000).WithMessage("Açıklama en fazla 2000 karakter olabilir.");

            RuleFor(product => product.PointsPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Kazanılan puan yüzdesi 0 veya daha büyük olmalıdır.")
                .LessThanOrEqualTo(100).WithMessage("Kazanılan puan yüzdesi 100'den fazla olamaz.");

            RuleFor(product => product.MaxPoints)
                .GreaterThanOrEqualTo(0).WithMessage("Maksimum puan 0 veya daha büyük olmalıdır.");

            // Category doğrulaması (Eğer gerekliyse)
            // RuleForEach(product => product.Categories)
            //    .SetValidator(new CategoryValidator());
        }
    }
}
