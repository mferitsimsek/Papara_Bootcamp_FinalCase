using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserSetRoleCommandHandler : IRequestHandler<AppUserSetRoleCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly UserManager<AppUser> _userManager;

        public AppUserSetRoleCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponseDTO<object?>> Handle(AppUserSetRoleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.AppUserId.ToString());
                if (user == null)
                {
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Kullanıcı bulunamadı." });
                }

                var roles = await _userManager.GetRolesAsync(user);
                var resultRemove = await _userManager.RemoveFromRolesAsync(user, roles); 

                if (!resultRemove.Succeeded)
                {
                    return new ApiResponseDTO<object?>(400, null, resultRemove.Errors.Select(e => e.Description).ToList());
                }

                var resultAdd = await _userManager.AddToRoleAsync(user, request.RoleName);

                if (resultAdd.Succeeded)
                {
                    return new ApiResponseDTO<object?>(200, null, new List<string> { "Rol başarıyla ayarlandı." });
                }

                return new ApiResponseDTO<object?>(400, null, resultAdd.Errors.Select(e => e.Description).ToList());
            }
            catch (Exception ex)
            {
                // Hata loglama yapılabilir
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Rol ayarlanırken bir hata oluştu." , ex.Message });
            }
        }
    }

}
