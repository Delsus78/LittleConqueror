using AutoMapper;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TerritoryDatabaseAdapter(
    TerritoryRepository territoryRepository, 
    IMapper mapper) : ITerritoryDatabasePort
{
    public async Task<Territory> CreateTerritory(Territory territory)
    {
        var entityEntry = await territoryRepository.CreateAsync(new TerritoryEntity
        {
            OwnerId = territory.Owner.Id
        });
        
        await territoryRepository.SaveAsync();
        
        return mapper.Map<Territory>(entityEntry.Entity);
    }

    public async Task<Territory> GetTerritoryOfUser(int userId)
    {
        var territoryEntity = await territoryRepository.GetAsync(entity => entity.OwnerId == userId);
        
        return mapper.Map<Territory>(territoryEntity);
    }
}