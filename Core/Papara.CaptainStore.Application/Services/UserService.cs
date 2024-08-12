using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Papara.CaptainStore.Application.Interfaces;
using Papara.CaptainStore.Domain.DTOs.AppUserDTOs;
using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Papara.CaptainStore.Application.Services
{
    public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<AppUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ApiResponseDTO<object?>> CheckIfEmailIsUsed(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            return new ApiResponseDTO<object?>(400, null, new List<string> { "Bu email adresi zaten kullanılıyor." });
        }
        return null;
    }

    public ApiResponseDTO<object?> ValidateAppUser(AppUser appUser)
    {
        // Burada FluentValidation veya özel validasyon işlemlerini uygulayabilirsiniz.
        // Örnek olarak:
        if (string.IsNullOrEmpty(appUser.Email))
        {
            return new ApiResponseDTO<object?>(400, null, new List<string> { "Email adresi boş olamaz." });
        }
        return null;
    }

    public async Task<ApiResponseDTO<object?>> CreateUserAsync(AppUser appUser, string password, string role)
    {
        var creationResult = await _userManager.CreateAsync(appUser, password);
        if (!creationResult.Succeeded)
        {
            return new ApiResponseDTO<object?>(400, null, creationResult.Errors.Select(e => e.Description).ToList());
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(appUser, role);
        if (!addToRoleResult.Succeeded)
        {
            return new ApiResponseDTO<object?>(400, null, addToRoleResult.Errors.Select(e => e.Description).ToList());
        }

        return new ApiResponseDTO<object?>(201, _mapper.Map<AppUserListDTO>(appUser), new List<string> { "Kullanıcı başarıyla oluşturuldu." });
    }

    public async Task<ApiResponseDTO<object?>> UpdateUserAsync(AppUser user, string? password)
    {
        if (!string.IsNullOrEmpty(password))
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded)
            {
                return new ApiResponseDTO<object?>(400, null, result.Errors.Select(e => e.Description).ToList());
            }
        }

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return new ApiResponseDTO<object?>(400, null, updateResult.Errors.Select(e => e.Description).ToList());
        }

        return new ApiResponseDTO<object?>(200, _mapper.Map<AppUserListDTO>(user), new List<string> { "Kullanıcı başarıyla güncellendi." });
    }
}


}
