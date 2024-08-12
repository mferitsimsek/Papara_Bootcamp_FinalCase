using FluentValidation;
using Papara.CaptainStore.Domain.Entities.CustomerEntities;

namespace Papara.CaptainStore.Application.Validators.CustomerValidators;

public class CustomerAccountValidator : AbstractValidator<CustomerAccount>
{
    public CustomerAccountValidator()
    {
        RuleFor(account => account.Balance)
            .GreaterThanOrEqualTo(0).WithMessage("Bakiye eksi değer olamaz.");
        RuleFor(account => account.Points)
           .GreaterThanOrEqualTo(0).WithMessage("Bakiye eksi değer olamaz.");
        RuleFor(account => account.AccountNumber)
            .NotEmpty().WithMessage("Hesap Numarası boş bırakılamaz.")
            .GreaterThanOrEqualTo(100000).WithMessage("Hesap Numarası en az 6 haneli olmalıdır.") 
            .LessThanOrEqualTo(99999999).WithMessage("Hesap Numarası en fazla 8 haneli olmalıdır.");
    }
}
