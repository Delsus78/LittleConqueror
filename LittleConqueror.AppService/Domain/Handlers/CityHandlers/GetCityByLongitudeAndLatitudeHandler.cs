using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.CityHandlers;

public interface IGetCityByLongitudeAndLatitudeHandler
{
    public Task<City> Handle(GetCityByLongitudeLatitudeQuery query);
}
public class GetCityByLongitudeAndLatitudeHandler(
    IOSMCityFetcherPort osmCityFetcher,
    ICityDatabasePort cityDatabase) : IGetCityByLongitudeAndLatitudeHandler
{
    public async Task<City> Handle(GetCityByLongitudeLatitudeQuery query)
    {
        var cityOSM = await osmCityFetcher.GetCityByLongitudeAndLatitude(query.Longitude, query.Latitude);
        if (string.IsNullOrEmpty(cityOSM.Name))
            throw new AppException("City not found", 404);
        
        var dbCity = await cityDatabase.GetCityById(cityOSM.OsmId) ?? await cityDatabase.AddCity(new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType,
            Name = cityOSM.Name,
            AddressType = cityOSM.AddressType,
            Population = cityOSM.Extratags?.Population ?? 0,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Geojson = cityOSM.Geojson
        });

        return dbCity;
    }
}