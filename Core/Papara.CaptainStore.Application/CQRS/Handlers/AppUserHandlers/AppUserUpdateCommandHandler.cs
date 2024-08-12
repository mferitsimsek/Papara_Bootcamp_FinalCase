using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserUpdateCommandHandler : IRequestHandler<AppUserUpdateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserService _userService;


        public AppUserUpdateCommandHandler(IMapper mapper, UserManager<AppUser> userManager, IUserService userService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _userService = userService;
        }

        public async Task<ApiResponseDTO<object?>> Handle(AppUserUpdateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(request.AppUserId.ToString());

                if (user == null)
                {
                    return new ApiResponseDTO<object?>(404, null, ["Güncelleme işlemi yapılacak kullanıcı bulunamadı."]);
                }

                _mapper.Map(request, user);

                return await _userService.UpdateUserAsync(user, request.Password);
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Güncelleme işlemi sırasında bir sorun oluştu.", ex.Message });
            }

        }
    }
}
