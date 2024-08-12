using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.CQRS.Queries.AppUserQueries;
using Papara.CaptainStore.Application.Interfaces.TokenService;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.CQRS.Handlers.AppUserHandlers
{
    public class AppUserLoginQueryHandler : IRequestHandler<AppUserLoginQueryRequest, ApiResponseDTO<AppUserLoginDTO?>>
    {
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AppUserLoginQueryHandler(IMapper mapper, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<ApiResponseDTO<AppUserLoginDTO?>> Handle(AppUserLoginQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                    return new ApiResponseDTO<AppUserLoginDTO?>(404, null, new List<string> { "Kullanıcı bulunamadı." });

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
                if (!result.Succeeded)
                    return new ApiResponseDTO<AppUserLoginDTO?>(500, null, new List<string> { "Kullanıcı adı ya da şifre hatalı. Lütfen tekrar deneyiniz." });

                var userRoles = await _userManager.GetRolesAsync(user);
                var userDto = _mapper.Map<AppUserLoginDTO>(user);

                var token = _tokenService.GenerateToken(userDto, userRoles);
                userDto.Token = token.Token;
                userDto.TokenExpiredDateTime = token.TokenExpiredDateTime;

                return new ApiResponseDTO<AppUserLoginDTO?>(200, userDto, new List<string> { "Giriş başarılı" });
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<AppUserLoginDTO?>(500, null, new List<string> { "Giriş işlemi sırasında bir sorun oluştu.", ex.Message });
            }
        }

    }
}
