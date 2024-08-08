using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class CityDatabaseAdapter(
    CityRepository cityRepository) : ICityDatabasePort
{
    public async Task<City?> GetCityById(int id)
        => await cityRepository.GetAsync(cityEntity => cityEntity.Id == id);

    public async Task<City?> AddCity(City city)
    {
        var entityEntry = await cityRepository.CreateAsync(city);
        
        await cityRepository.SaveAsync();
        
        return entityEntry.Entity;
    }

    public async Task SetTerritoryId(int cityId, int territoryId)
    {
        await ValidateIfNotExistAsync(cityId);
        
        var city = await cityRepository.GetByIdAsync(cityId);
        if (city == null)
            throw new AppException("City not found", 404);
        
        city.TerritoryId = territoryId;
        
        await cityRepository.UpdateAsync(city);
    }
    
    private async Task ValidateIfNotExistAsync(int Id)
    {
        var existingEntity = await cityRepository.GetAsync(entity => entity.Id == Id);
        if (existingEntity == null)
            throw new AppException($"Entity with the id : {Id} dont exist.", 404);
    }
}