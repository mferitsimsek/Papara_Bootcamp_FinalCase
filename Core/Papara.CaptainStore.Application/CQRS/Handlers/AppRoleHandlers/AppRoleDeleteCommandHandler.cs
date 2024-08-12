using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppRoleCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppRoleEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppRoleHandlers
{

    public class AppRoleDeleteCommandHandler : IRequestHandler<AppRoleDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly RoleManager<AppRole> _roleManager;
        public AppRoleDeleteCommandHandler(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResponseDTO<object?>> Handle(AppRoleDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _roleManager.FindByIdAsync(request.AppRoleId.ToString());
                if (user == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek rol bulunamadı." });
                }

                var result = await _roleManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
                }

                return new ApiResponseDTO<object?>(400, null, result.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception ex)
            {
                // Hata loglaması yapılabilir
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
