﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Papara.CaptainStore.Application;

public class JwtManager
{
    public static Session GetSession(HttpContext context)
    {
        Session session = new Session();
        var identity = context.User.Identity as ClaimsIdentity;
        var claims = identity.Claims;
        session.UserName = GetClaimValue(claims, "UserName");
        session.Status = Convert.ToInt32(GetClaimValue(claims, "Status"));
        session.UserId = GetClaimValue(claims, "UserId");
        session.Role = GetClaimValue(claims, "Role");
        session.Email = GetClaimValue(claims, "Email");
        session.CustomerId = Convert.ToInt64(GetClaimValue(claims, "CustomerId"));
        session.CustomerNumber = Convert.ToInt32(GetClaimValue(claims, "CustomerNumber"));
        session.CustomerName = GetClaimValue(claims, "CustomerName");
        session.CustomerEmail = GetClaimValue(claims, "CustomerEmail");
        return session;
    }

    private static string GetClaimValue(IEnumerable<Claim> claims, string name)
    {
        var claim = claims.FirstOrDefault(c => c.Type == name);
        return claim?.Value;
    }
}
