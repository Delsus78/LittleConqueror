using LittleConqueror.AppService.Domain.Models.Configs;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IPopulationHappinessConfigsDatabasePort
{
    Task<List<PopHappinessConfig>> GetAllPopHappinessConfigs(bool forceRefresh = false);
    Task<PopHappinessConfig> GetPopHappinessConfigByType(PopHappinessType type, bool forceRefresh = false);
}