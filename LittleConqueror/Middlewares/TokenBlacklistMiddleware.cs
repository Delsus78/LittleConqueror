using System.IdentityModel.Tokens.Jwt;
using LittleConqueror.Infrastructure.JwtAdapters;

namespace LittleConqueror.Middlewares;

public class TokenBlacklistMiddleware(RequestDelegate next, ITokenManagerService TokenBlacklist)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("Authorization", out var tokenValue))
        {
            var token = tokenValue.ToString().Replace("Bearer ", "");
            
            var tokenJTI = new JwtSecurityTokenHandler()
                .ReadJwtToken(token)
                .Claims
                .First(claim => claim.Type == JwtRegisteredClaimNames.Jti)
                .Value;
            
            if (TokenBlacklist.IsTokenBlacklisted(tokenJTI))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }

        await next(context);
    }
}