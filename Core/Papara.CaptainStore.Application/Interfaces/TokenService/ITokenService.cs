using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.Interfaces.TokenService
{
    public interface ITokenService
    {
        LoginInfoDTO GenerateToken(AppUserLoginDTO appUserLoginDTO, IList<string> userRoles);
    }
}
