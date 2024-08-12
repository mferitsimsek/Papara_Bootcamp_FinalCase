using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.Interfaces
{
    public interface ITokenService
    {
        LoginInfoDTO GenerateToken(AppUserLoginDTO appUserLoginDTO, IList<string> userRoles);
    }
}
