using FluentValidation;
using Papara.CaptainStore.Application.CQRS.Commands.PaymentCommands;

namespace Papara.CaptainStore.Application.Validators.PaymentValidators
{
    public class PaymentRequestValidator : AbstractValidator<PaymentCommandRequest>
    {
        public PaymentRequestValidator()
        {
            RuleFor(x => x.CardNumber).CreditCard().WithMessage("Invalid card number.");
            RuleFor(x => x.CardHolderName).NotEmpty().WithMessage("Card holder name is required.");
            RuleFor(x => x.ExpiryMonth).NotEmpty().Length(2).WithMessage("Invalid expiry month.");
            RuleFor(x => x.ExpiryYear).NotEmpty().Length(4).WithMessage("Invalid expiry year.");
            RuleFor(x => x.CVV).NotEmpty().Length(3).WithMessage("Invalid CVV.");
        }
    }
}
