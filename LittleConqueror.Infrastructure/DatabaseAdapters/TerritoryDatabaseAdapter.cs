using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TerritoryDatabaseAdapter(
    TerritoryRepository territoryRepository) : ITerritoryDatabasePort
{
    public async Task<Territory> CreateTerritory(Territory territory)
    {
        var entityEntry = await territoryRepository.CreateAsync(territory);
        
        await territoryRepository.SaveAsync();
        
        return entityEntry.Entity;
    }

    public async Task<Territory?> GetTerritoryOfUser(long userId)
    {
        var response = await territoryRepository
            .GetAsync(new GetTerritoryOfUserWithAllCitiesWithoutGeoJsonSpec(userId));
        return response.FirstOrDefault();
    }
    
    public async Task<List<City>> GetTerritoryCitiesFullDataOfUser(long userId)
    {
        var response = await territoryRepository
            .GetAsync(new GetTerritoryCitiesOfUserWithFullDataSpec(userId));
        return response[0].Cities;
    }
    
    public async Task<Territory?> GetTerritoryById(long territoryId)
        => await territoryRepository.GetByIdAsync(territoryId);
}