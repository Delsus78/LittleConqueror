using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;

public sealed class GetTerritoryOfUserWithAllCitiesWithoutGeoJsonSpec
    : BaseSpecification<Territory>
{
    public GetTerritoryOfUserWithAllCitiesWithoutGeoJsonSpec(long userId)
        : base(t => t.OwnerId == userId)
    {
        AddInclude(t => t.Cities);
        ApplySelect(t => new Territory
        {
            Id = t.Id,
            OwnerId = t.OwnerId,
            Cities = t.Cities.Select(c => new City
            {
                Id = c.Id,
                OsmType = c.OsmType,
                AddressType = c.AddressType,
                TerritoryId = c.TerritoryId,
                Action = CityWithActionSpecExtensions.PopulateWithCity(c.Action, c),
                Name = c.Name,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                Population = c.Population
            }).ToList()
        });
    }
}

public sealed class GetTerritoryCitiesOfUserWithFullDataSpec
    : BaseSpecification<Territory>
{
    public GetTerritoryCitiesOfUserWithFullDataSpec(long userId) 
        : base(t => t.OwnerId == userId)
    {
        AddInclude(t => t.Cities);
        ApplySelect(t => new Territory
        {
            Id = t.Id,
            OwnerId = t.OwnerId,
            Cities = t.Cities.Select(c => new City
            {
                Id = c.Id,
                OsmType = c.OsmType,
                AddressType = c.AddressType,
                TerritoryId = c.TerritoryId,
                Action = CityWithActionSpecExtensions.PopulateWithCity(c.Action, c),
                Name = c.Name,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                Population = c.Population,
                Geojson = c.Geojson
            }).ToList()
        });
    }
}