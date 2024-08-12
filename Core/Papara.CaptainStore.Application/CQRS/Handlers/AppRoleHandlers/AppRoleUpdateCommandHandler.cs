using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppRoleHandlers
{
    public class AppRoleUpdateCommandHandler : IRequestHandler<AppRoleUpdateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IValidator<AppRole> _validator;

        public AppRoleUpdateCommandHandler(IMapper mapper, RoleManager<AppRole> roleManager, IValidator<AppRole> validator)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _validator = validator;
        }

        public async Task<ApiResponseDTO<object?>> Handle(AppRoleUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var roleToUpdate = await _roleManager.FindByIdAsync(request.AppRoleId.ToString());
                if (roleToUpdate == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Güncellenecek rol bulunamadı." });
                }

                var roleCheckResult = await CheckRoleIsExist(request.Name);
                if (roleCheckResult != null) return roleCheckResult;


                _mapper.Map(request, roleToUpdate);

                var validationResult = ValidateAppRole(roleToUpdate);
                if (validationResult != null) return validationResult;

                var updateResult = await _roleManager.UpdateAsync(roleToUpdate);
                if (!updateResult.Succeeded)
                {
                    return new ApiResponseDTO<object?>(400, null, updateResult.Errors.Select(e => e.Description).ToList());
                }

                return new ApiResponseDTO<object?>(200, _mapper.Map<AppRoleListDTO>(roleToUpdate), new List<string> { "Güncelleme işlemi başarılı." });

            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
        private async Task<ApiResponseDTO<object?>?> CheckRoleIsExist(string roleName)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(roleName);
            if (isRoleExist)
            {
                return new ApiResponseDTO<object?>(303, null, new List<string> { "Güncellemek istediğiniz rol adı sistemde kayıtlıdır." });
            }
            return null;
        }


        private ApiResponseDTO<object?>? ValidateAppRole(AppRole appRole)
        {
            var validationResult = _validator.Validate(appRole);
            if (!validationResult.IsValid)
            {
                return new ApiResponseDTO<object?>(400, null, validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            return null;
        }

    }
}
