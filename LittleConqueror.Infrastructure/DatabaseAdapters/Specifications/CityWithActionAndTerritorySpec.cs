using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;

public sealed class SetCityActionWithOwnerIdSpec
    : BaseSpecification<City>
{
    public SetCityActionWithOwnerIdSpec(long cityId) 
        : base(entity => entity.Id == cityId)
    {
        AddInclude(c => c.Action);
        ApplySelect(c => new City
        {
            Id = c.Id,
            OsmType = c.OsmType,
            AddressType = c.AddressType,
            Territory = c.Territory != null ? new Territory
            {
                Id = c.Territory.Id,
                OwnerId = c.Territory.OwnerId
            } : null,
            TerritoryId = c.TerritoryId,
            Action = CityWithActionSpecExtensions.PopulateWithCity(c.Action, c),
            Population = c.Population,
            Latitude = c.Latitude,
            Longitude = c.Longitude
        });
    }
}