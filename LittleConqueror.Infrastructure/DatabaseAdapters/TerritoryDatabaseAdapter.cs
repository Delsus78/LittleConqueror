using AutoMapper;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.DrivenPorts.Specifications;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TerritoryDatabaseAdapter(
    TerritoryRepository territoryRepository, 
    IMapper mapper) : ITerritoryDatabasePort
{
    public async Task<Territory> CreateTerritory(Territory territory)
    {
        var entityEntry = await territoryRepository.CreateAsync(territory);
        
        await territoryRepository.SaveAsync();
        
        return entityEntry.Entity;
    }

    public async Task<Territory?> GetTerritoryOfUser(int userId)
        => (await territoryRepository.GetAsync(new TerritoryFromUserIdWithCitiesSpec(userId))).FirstOrDefault();
    

    public async Task<Territory?> GetTerritoryById(int territoryId)
        => await territoryRepository.GetByIdAsync(territoryId);
    
    private async Task ValidateIfNotExistAsync(int Id)
    {
        var existingEntity = await territoryRepository.GetAsync(entity => entity.Id == Id);
        if (existingEntity == null)
            throw new AppException($"Entity with the id : {Id} dont exist.", 404);
    }
}