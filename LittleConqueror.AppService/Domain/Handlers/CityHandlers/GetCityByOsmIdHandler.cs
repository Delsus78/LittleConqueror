using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.CityHandlers;

public interface IGetCityByOsmIdHandler
{
    Task<City> Handle(GetCityByOsmIdQuery query);
}
public class GetCityByOsmIdHandler(
    IOSMCityFetcherPort osmCityFetcher,
    ICityDatabasePort cityDatabase) : IGetCityByOsmIdHandler
{
    public async Task<City> Handle(GetCityByOsmIdQuery query)
    {
        var cityOSM = await osmCityFetcher.GetCityByOsmId(query.OsmId, query.OsmType);
        if (string.IsNullOrEmpty(cityOSM.Name))
            throw new AppException("City not found", 404);
        
        var dbCity = await cityDatabase.GetCityById(cityOSM.OsmId);
        if (dbCity == null)
            await cityDatabase.AddCity(new City
            {
                Id = cityOSM.OsmId,
                OsmType = cityOSM.OsmIdType,
                Name = cityOSM.Name,
                Latitude = cityOSM.Lat,
                Longitude = cityOSM.Lon,
                Geojson = cityOSM.Geojson,
                Population = cityOSM.Extratags?.Population ?? 0
            });
        
        return new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType,
            Name = cityOSM.Name,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Population = cityOSM.Extratags?.Population ?? 0,
            Geojson = cityOSM.Geojson
        };
    }
}