﻿using Papara.CaptainStore.Domain.DTOs;

namespace Papara.CaptainStore.Application.Services.TokenServices
{
    public interface ITokenService
    {
        LoginInfoDTO GenerateToken(AppUserLoginDTO appUserLoginDTO, IList<string> userRoles);
    }
}
