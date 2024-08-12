using FluentValidation;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Application.Validators.AppRoleValidators
{
    public class AppRoleValidator : AbstractValidator<AppRole>
    {
        public AppRoleValidator()
        {
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("Rol adı en az 4 karakter olmalı.");
        }
    }
}
