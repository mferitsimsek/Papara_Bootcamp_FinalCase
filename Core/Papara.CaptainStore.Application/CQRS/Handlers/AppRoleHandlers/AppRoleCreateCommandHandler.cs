using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppRoleHandlers
{
    public class AppRoleCreateCommandHandler : IRequestHandler<AppRoleCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IValidator<AppRole> _validator;

        public AppRoleCreateCommandHandler(IMapper mapper, RoleManager<AppRole> roleManager, IValidator<AppRole> validator)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _validator = validator;
        }
        public async Task<ApiResponseDTO<object?>> Handle(AppRoleCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roleCheckResult = await CheckRoleIsExist(request.Name);
                if (roleCheckResult != null) return roleCheckResult;

                var appRole = _mapper.Map<AppRole>(request);
                var validationResult = ValidateAppRole(appRole);
                if (validationResult != null) return validationResult;

                var creationResult = await _roleManager.CreateAsync(appRole);
                if (!creationResult.Succeeded)
                    return new ApiResponseDTO<object?>(400, null, creationResult.Errors.Select(e => e.Description).ToList());

                return new ApiResponseDTO<object?>(201, _mapper.Map<AppRoleListDTO>(appRole), new List<string> { "Kayıt işlemi başarılı." });

            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }

        private async Task<ApiResponseDTO<object?>?> CheckRoleIsExist(string roleName)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (isRoleExist)
            {
                return new ApiResponseDTO<object?>(303, null, new List<string> { "Eklemek istediğiniz rol sistemde kayıtlıdır." });
            }
            return null;
        }

        private ApiResponseDTO<object?>? ValidateAppRole(AppRole appRole)
        {
            var validationResult = _validator.Validate(appRole);
            if (!validationResult.IsValid)
            {
                return new ApiResponseDTO<object?>(303, null, validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            return null;
        }
    }
}
