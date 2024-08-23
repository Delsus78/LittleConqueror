using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LittleConqueror.Infrastructure.JwtAdapters;

public class JwtTokenProviderAdapter(IOptions<AppSettings> options, ITokenManagerService tokenManagerService) : IJwtTokenProviderPort
{
    private readonly AppSettings appSettings = options.Value;
    
    public async Task<string> GenerateJwtToken(AuthUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var expiration = DateTime.UtcNow.AddMinutes(appSettings.ExpirationInMinutes);
            var tokenJti = Guid.NewGuid().ToString();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserId?.ToString() ?? ""),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(JwtRegisteredClaimNames.Jti, tokenJti),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("AppName", "LittleConqueror")
                }),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            
            tokenManagerService.SetTokenJTI(tokenJti, user.Id);
            
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
}