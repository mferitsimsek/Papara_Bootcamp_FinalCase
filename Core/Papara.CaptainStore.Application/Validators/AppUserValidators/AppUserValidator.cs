using FluentValidation;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.Validators.AppUserValidators
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(x => x.UserName).MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalı.");
            RuleFor(x => x.Email).NotEmpty().NotNull().WithMessage("Email boş bırakılamaz.");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage("Ad alanı boş bırakılamaz.");
            RuleFor(x => x.LastName).MinimumLength(2).WithMessage("Soyad alanı boş bırakılamaz.");
            RuleFor(x => x.CityId).NotEmpty().NotNull().WithMessage("Şehir alanı boş bırakılamaz.");
            RuleFor(x => x.CountryId).NotEmpty().NotNull().WithMessage("Ülke alanı boş bırakılamaz.");
            RuleFor(x => x.DistrictId).NotEmpty().NotNull().WithMessage("İlçe alanı boş bırakılamaz.");
        }
    }
}
