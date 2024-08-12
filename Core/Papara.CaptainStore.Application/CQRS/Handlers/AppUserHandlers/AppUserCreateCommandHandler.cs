using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Commands.AppUserCommands;
using Papara.CaptainStore.Application.Events;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.DTOs.AppUserDTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserCreateCommandHandler : IRequestHandler<AppUserCreateCommandRequest, ApiResponseDTO<object?>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public AppUserCreateCommandHandler(IMapper mapper, IMediator mediator, IUserService userService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userService = userService;
        }
        public async Task<ApiResponseDTO<object?>> Handle(AppUserCreateCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var emailCheckResult = await _userService.CheckIfEmailIsUsed(request.Email);
                if (emailCheckResult != null) return emailCheckResult;

                var appUser = _mapper.Map<AppUser>(request);
                var validationResult = _userService.ValidateAppUser(appUser);
                if (validationResult != null) return validationResult;

                var creationResult = await _userService.CreateUserAsync(appUser, request.Password, "User");
                if (creationResult.status != 201)
                {
                    return creationResult;
                }
                await _mediator.Publish(new UserCreatedEvent(appUser.Id, request));

                return creationResult; 
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<object?>(500, null, new List<string> { "Kayıt işlemi sırasında bir sorun oluştu." , ex.Message });
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
