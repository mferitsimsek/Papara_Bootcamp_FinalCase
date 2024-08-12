using AutoMapper;
using FluentValidation;
using MediatR;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Application.Helpers;
using Papara.CaptainStore.Application.Interfaces.UserService;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AdminUserCreateCommandHandler : IRequestHandler<AdminUserCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUser> _validator;

        public AdminUserCreateCommandHandler(IMapper mapper, IUserService userService, IValidator<AppUser> validator)
        {
            _mapper = mapper;
            _userService = userService;
            _validator = validator;
        }
        public async Task<ApiResponseDTO<object?>> Handle(AdminUserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var emailCheckResult = await _userService.CheckIfEmailIsUsed(request.Email);
                if (emailCheckResult != null) return emailCheckResult;

                var result = await ValidationHelper.ValidateAndMapForCreateAsync(
                            request,
                            _mapper,
                            _validator,
                            () => Task.FromResult<AppUser>(new AppUser())
                        );
                if (result.status != 200)
                {
                    return result;
                }

                var appUser = result.data as AppUser;

                return await _userService.CreateUserAsync(appUser, request.Password, "Admin");

            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }
    }
}
