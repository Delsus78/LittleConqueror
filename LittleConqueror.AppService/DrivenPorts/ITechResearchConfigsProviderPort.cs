using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITechResearchConfigsProviderPort
{
    Task<List<TechConfig>> GetAll(bool forceRefresh = false);
    Task<TechConfig> GetByType(TechResearchType type, bool forceRefresh = false);
    Task InitTechConfigs(IEnumerable<TechConfig> techConfigs);
}