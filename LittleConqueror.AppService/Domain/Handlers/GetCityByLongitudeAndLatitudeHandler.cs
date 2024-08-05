using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers;

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
        
        var dbCity = await cityDatabase.GetCityById(cityOSM.PlaceId); 
        
        if (dbCity == null)
            await cityDatabase.AddCity(new City
            {
                Id = cityOSM.PlaceId,
                Name = cityOSM.Name,
                Population = cityOSM.Extratags?.Population ?? 0
            });
        
        return new City
        {
            Id = cityOSM.PlaceId,
            Country = cityOSM.Address?.Country ?? string.Empty,
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