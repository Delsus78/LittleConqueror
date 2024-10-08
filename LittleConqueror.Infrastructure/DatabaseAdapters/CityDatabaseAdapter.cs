using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class CityDatabaseAdapter(
    CityRepository cityRepository) : ICityDatabasePort
{
    public async Task<City?> GetCityById(long id)
        => (await cityRepository.GetAsync(new CityWithActionSpec(id))).FirstOrDefault();

    public async Task<City?> GetCityWithActionAndTerritoryOwnerId(long id)
    {
        return (await cityRepository.GetAsync(new SetCityActionWithOwnerIdSpec(id))).FirstOrDefault();
    }

    public async Task<City> AddCity(City city)
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
    
    private async Task ValidateIfNotExistAsync(long Id)
    {
        var existingEntity = await cityRepository.GetAsync(entity => entity.Id == Id);
        if (existingEntity == null)
            throw new AppException($"Entity with the id : {Id} dont exist.", 404);
    }
}