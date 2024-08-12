using Microsoft.IdentityModel.Tokens;
using Papara.CaptainStore.Application.Interfaces.TokenService;
using Papara.CaptainStore.Domain.Consts;
using Papara.CaptainStore.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Papara.CaptainStore.Application.Services
{
    public class TokenService : ITokenService
    {
        public LoginInfoDTO GenerateToken(AppUserLoginDTO user, IList<string> userRoles)
        {
            Claim[] claims = GetClaims(user, userRoles);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key));
            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefaults.Expire);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtTokenDefaults.ValidIssuer,
                audience: JwtTokenDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expireDate,
                signingCredentials: signinCredentials);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            return new LoginInfoDTO
            {
                Token = tokenHandler.WriteToken(token),
                TokenExpiredDateTime = expireDate,
            };
        }
        private static Claim[] GetClaims(AppUserLoginDTO user, IList<string> userRoles)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserName", user.UserName),
                new Claim("UserId", user.AppUserId.ToString()),
                new Claim("Email", user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            if (userRoles.Any())
            {
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            return claims.ToArray();
        }
    }
}
