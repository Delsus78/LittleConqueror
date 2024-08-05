using AutoMapper;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class CityDatabaseAdapter(
    CityRepository cityRepository, 
    IMapper mapper) : ICityDatabasePort
{
    public async Task<City?> GetCityById(int id)
    {
        var entity = await cityRepository.GetAsync(cityEntity => cityEntity.Id == id);
        return entity == null ? null : mapper.Map<City>(entity);
    }

    public async Task<City?> AddCity(City city)
    {
        var entityEntry = await cityRepository.CreateAsync(new CityEntity
        {
            Id = city.Id,
            Name = city.Name,
            Population = city.Population
        });
        
        await cityRepository.SaveAsync();
        
        return mapper.Map<City?>(entityEntry.Entity);
    }
}