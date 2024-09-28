using LittleConqueror.AppService.Domain.Models.Configs;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITechResearchConfigsDatabasePort
{
    Task<List<TechConfig>> GetAllTechConfigs(bool forceRefresh = false);
    Task<TechConfig> GetTechConfigByType(TechResearchType type, bool forceRefresh = false);
}