using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class GameConfigsDatabaseAdapter(
    ConfigsRepository configsRepository) : ITechResearchConfigsDatabasePort, IPopulationHappinessConfigsDatabasePort
{
    public async Task<List<TechConfig>> GetAllTechConfigs(bool forceRefresh = false)
        => await configsRepository.GetAllTechConfigs(forceRefresh);

    public async Task<PopHappinessConfig> GetPopHappinessConfigByType(PopHappinessType type, bool forceRefresh = false)
        => await configsRepository.GetPopHappinessConfig(type, forceRefresh)
              ?? throw new AppException($"PopHappinessConfig with type {type} not found", 404);

    public async Task<TechConfig> GetTechConfigByType(TechResearchType type, bool forceRefresh = false)
        => await configsRepository.GetTechConfig(type, forceRefresh)
              ?? throw new AppException($"TechConfig with type {type} not found", 404);

    public async Task<List<PopHappinessConfig>> GetAllPopHappinessConfigs(bool forceRefresh)
        => await configsRepository.GetAllPopHappinessConfigs(forceRefresh);
    
}