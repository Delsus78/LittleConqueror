using LittleConqueror.AppService.Domain.Models;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IJwtTokenProviderPort
{
    Task<string> GenerateJwtToken(AuthUser user);
}