using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserPasswordResetCommandHandler : IRequestHandler<AppUserPasswordResetCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ISessionContext _sessionContext;
        public AppUserPasswordResetCommandHandler(UserManager<AppUser> userManager, ISessionContext sessionContext)
        {
            _userManager = userManager;
            _sessionContext = sessionContext;
        }

        public async Task<ApiResponseDTO<object?>> Handle(AppUserPasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(_sessionContext.Session.UserId);
                if (user == null)
                    return new ApiResponseDTO<object?>(404, null, ["Kullanıcı bulunamadı."]);

                var updatePasswordResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (!updatePasswordResult.Succeeded)
                    return new ApiResponseDTO<object?>(500, null, ["Şifre güncellenemiyor. Lütfen daha sonra tekrar deneyin."]);

                return new ApiResponseDTO<object?>(200, null, ["Şifre başarıyla değiştirildi."]);

            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Şifre sıfırlama işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
