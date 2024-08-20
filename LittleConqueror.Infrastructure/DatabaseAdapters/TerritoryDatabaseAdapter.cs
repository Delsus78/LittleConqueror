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
        => (await territoryRepository.GetAsync(new TerritoryFromUserIdWithCitiesAndActionSpec(userId)))[0];
    

    public async Task<Territory?> GetTerritoryById(long territoryId)
        => await territoryRepository.GetByIdAsync(territoryId);
}