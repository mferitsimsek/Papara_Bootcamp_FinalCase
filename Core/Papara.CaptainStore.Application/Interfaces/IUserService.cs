using Papara.CaptainStore.Domain.DTOs;
using Papara.CaptainStore.Domain.Entities.AppUserEntities;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponseDTO<object?>> CheckIfEmailIsUsed(string email);
        ApiResponseDTO<object?> ValidateAppUser(AppUser appUser);
        Task<ApiResponseDTO<object?>> CreateUserAsync(AppUser appUser, string password, string role);
        Task<ApiResponseDTO<object?>> UpdateUserAsync(AppUser user, string? password);
    }

}
