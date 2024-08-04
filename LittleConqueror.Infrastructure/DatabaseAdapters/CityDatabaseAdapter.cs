using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;
using LittleConqueror.Persistence.Entities;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class CityDatabaseAdapter(CityRepository cityRepository) : ICityDatabasePort
{
    public async Task<City?> GetCityById(int id)
    {
        var entity = await cityRepository.GetAsync(cityEntity => cityEntity.Id == id);

        if (entity == null)
            return null;
        
        return new City
        {
            Id = entity.Id,
            Name = entity.Name,
            Population = entity.Population
        };
    }

    public async Task<City?> AddCity(City city)
    {
        await cityRepository.CreateAsync(new CityEntity
        {
            Id = city.Id,
            Name = city.Name,
            Population = city.Population
        });
        
        await cityRepository.SaveAsync();
        
        return city;
    }
}