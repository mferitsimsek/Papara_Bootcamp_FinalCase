using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Application.Events;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserDeleteCommandHandler : IRequestHandler<AppUserDeleteCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        public AppUserDeleteCommandHandler(UserManager<AppUser> userManager, IMediator mediator, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponseDTO<object?>> Handle(AppUserDeleteCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.AppUserId.ToString());
                if (user == null)
                    return new ApiResponseDTO<object?>(404, null, new List<string> { "Silinecek kullanıcı bulunamadı." });

                user.IsDeleted = true;
                await _unitOfWork.AppUserRepository.UpdateAsync(user);
                //Kullanıcı silme  başarılı ise Customer Account silme event'i tetikleniyor.
                await _mediator.Publish(new UserDeletedEvent(user.Id));
                await _unitOfWork.Complete();
                return new ApiResponseDTO<object?>(200, null, new List<string> { "Silme işlemi başarılı." });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Silme işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
