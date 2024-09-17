using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.ConfigsAdapters;

public class TechResearchConfigsProviderAdapter(
    ConfigsRepository configsRepository) : ITechResearchConfigsProviderPort
{
    public Task<List<TechConfig>> GetAll(bool forceRefresh = false)
        => configsRepository.GetAllTechConfigs(forceRefresh);

    public async Task<TechConfig> GetByType(TechResearchType type, bool forceRefresh = false)
        => await configsRepository.GetTechConfig(type, forceRefresh)
              ?? throw new AppException($"TechConfig with type {type} not found", 404);
    
    public async Task InitTechConfigs(IEnumerable<TechConfig> techConfigs)
        => await configsRepository.InitAsync(techConfigs);
}