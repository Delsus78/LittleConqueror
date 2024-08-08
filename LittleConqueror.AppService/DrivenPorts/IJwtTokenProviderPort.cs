using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IJwtTokenProviderPort
{
    Task<string> GenerateJwtToken(AuthUser user);
}