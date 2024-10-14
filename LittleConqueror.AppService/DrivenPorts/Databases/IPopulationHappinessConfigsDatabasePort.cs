using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IPopulationHappinessConfigsDatabasePort
{
    Task<List<PopHappinessConfig>> GetAllPopHappinessConfigs(bool forceRefresh = false);
    Task<PopHappinessConfig> GetPopHappinessConfigByType(ResourceType type, bool forceRefresh = false);
}