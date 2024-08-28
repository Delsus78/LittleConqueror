using LittleConqueror.AppService.Domain.Models.Entities;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;

public sealed class CityWithActionSpec 
    : BaseSpecification<City>
{
    public CityWithActionSpec(long cityId) 
        : base(entity => entity.Id == cityId)
    {
        AddInclude(c => c.Action);
        ApplySelect(city => new City
        {
            Id = city.Id,
            OsmType = city.OsmType,
            AddressType = city.AddressType,
            TerritoryId = city.TerritoryId,
            Territory = city.Territory != null ? new Territory
            {
                Id = city.Territory.Id,
                OwnerId = city.Territory.OwnerId
            } : null,
            Action = CityWithActionSpecExtensions.PopulateWithCity(city.Action, city),
            Name = city.Name,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            Geojson = city.Geojson,
            Population = city.Population
        });
        
    }
}

public static class CityWithActionSpecExtensions
{
    public static ActionEntities.Action? PopulateWithCity(ActionEntities.Action? action, City city)
    {
        if (action == null) return null;
        
        action.City = city;
        return action;
    }
}
