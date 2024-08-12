using FluentValidation;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
