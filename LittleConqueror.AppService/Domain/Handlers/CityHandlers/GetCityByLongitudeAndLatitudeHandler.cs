using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
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
        
        var dbCity = await cityDatabase.GetCityById(cityOSM.OsmId); 
        
        if (dbCity == null)
            await cityDatabase.AddCity(new City
            {
                Id = cityOSM.OsmId,
                OsmType = cityOSM.OsmIdType[0],
                Name = cityOSM.Name,
                Population = cityOSM.Extratags?.Population ?? 0
            });
        
        return new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType[0],
            Name = cityOSM.Name,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Population = cityOSM.Extratags?.Population ?? 0,
            Geojson = new Geojson
            {
                Type = cityOSM.Geojson?.Type ?? string.Empty,
                Coordinates = cityOSM.Geojson?.Coordinates ?? new List<List<List<double>>>()
            }
        };
    }
}