using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;
using LittleConqueror.Infrastructure.Repositories;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class CityDatabaseAdapter(
    CityRepository cityRepository) : ICityDatabasePort
{
    public async Task<City?> GetCityById(long id)
        => await cityRepository.GetAsync(cityEntity => cityEntity.Id == id);

    public async Task<City?> GetCityWithAction(long id)
    {
        return (await cityRepository.GetAsync(new CityWithActionSpec(id)))[0];
    }

    public async Task<City?> AddCity(City city)
    {
        var entityEntry = await cityRepository.CreateAsync(city);
        
        await cityRepository.SaveAsync();
        
        return entityEntry.Entity;
    }

    public async Task SetTerritoryId(long cityId, long territoryId)
    {
        await ValidateIfNotExistAsync(cityId);
        
        var city = await cityRepository.GetByIdAsync(cityId);
        if (city == null)
            throw new AppException("City not found", 404);
        
        city.TerritoryId = territoryId;
        
        await cityRepository.UpdateAsync(city);
    }
    
    public async Task SetAction(City city, ActionEntities.Action action)
    {
        await ValidateIfNotExistAsync(city.Id);
        
        city.Action = action;
        
        await cityRepository.UpdateAsync(city);
    }
    
    private async Task ValidateIfNotExistAsync(long Id)
    {
        var existingEntity = await cityRepository.GetAsync(entity => entity.Id == Id);
        if (existingEntity == null)
            throw new AppException($"Entity with the id : {Id} dont exist.", 404);
    }
}