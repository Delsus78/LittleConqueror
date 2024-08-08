using System.Text;
using LittleConqueror.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace LittleConqueror.Authentication;

public static class JwtConfiguration
{
    public static IServiceCollection ConfigureJwt(
        this IServiceCollection services,
        AppSettings? appSettings)
    {
        // Configure JWT authentication
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
            });
        
        return services;
    }
}