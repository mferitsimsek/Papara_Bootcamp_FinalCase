using FluentValidation;
using Papara.CaptainStore.Domain.Entities.CategoryEntities;

namespace Papara.CaptainStore.Application.Validators.CategoryValidators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş olamaz.")
                .MaximumLength(255).WithMessage("Kategori adı en fazla 255 karakter olabilir.");

            RuleFor(category => category.Url)
                .NotEmpty().WithMessage("URL boş olamaz.")
                .MaximumLength(255).WithMessage("URL en fazla 255 karakter olabilir.");

            RuleFor(category => category.Tag)
                .NotEmpty().WithMessage("Etiket boş olamaz.")
                .MaximumLength(255).WithMessage("Etiket en fazla 255 karakter olabilir.");
        }
    }
}
