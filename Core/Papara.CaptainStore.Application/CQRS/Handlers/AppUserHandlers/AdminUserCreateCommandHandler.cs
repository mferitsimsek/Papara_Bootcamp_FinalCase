using AutoMapper;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AdminUserCreateCommandHandler : IRequestHandler<AdminUserCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AdminUserCreateCommandHandler(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(AdminUserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var emailCheckResult = await _userService.CheckIfEmailIsUsed(request.Email);
                if (emailCheckResult != null) return emailCheckResult;

                var appUser = _mapper.Map<AppUser>(request);
                var validationResult = _userService.ValidateAppUser(appUser);
                if (validationResult != null) return validationResult;

                return await _userService.CreateUserAsync(appUser, request.Password, "Admin");

            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu.", ex.Message });
                //return HandleException(ex);
            }
        }

        //private IDTO<object?> HandleException(Exception ex)
        //{
        //    // Exception logging veya daha ileri işlem yapılabilir
        //    return new IDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu." });
        //}
    }
}
