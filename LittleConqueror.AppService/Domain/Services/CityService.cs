using LittleConqueror.AppService.Domain.DrivenModels;
using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.DrivingPorts;
using Geojson = LittleConqueror.AppService.DomainEntities.Geojson;

namespace LittleConqueror.AppService.Domain.Services;

public class CityService(IOSMCityFetcherPort osmCityFetcher, ICityDatabasePort cityDatabase) : ICityService
{
    public async Task<City> GetCityByLongitudeAndLatitude(double longitude, double latitude)
    {
        var cityOSM = await osmCityFetcher.GetCityByLongitudeAndLatitude(longitude, latitude);
        
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