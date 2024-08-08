using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers.CityHandlers;

public class GetCityByOsmIdHandlerTest
{
    private readonly Mock<IOSMCityFetcherPort> _osmCityFetcher;
    private readonly Mock<ICityDatabasePort> _cityDatabase;
    private readonly GetCityByOsmIdHandler _getCityByOsmIdHandler;
    
    public GetCityByOsmIdHandlerTest()
    {
        _osmCityFetcher = new Mock<IOSMCityFetcherPort>();
        _cityDatabase = new Mock<ICityDatabasePort>();
        
        _getCityByOsmIdHandler = new GetCityByOsmIdHandler(
            _osmCityFetcher.Object,
            _cityDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetCityByOsmIdQuery_ReturnsCityWithNoCityInDb(
        GetCityByOsmIdQuery query, CityOSM cityOSM)
    {
        // arrange
        
        _osmCityFetcher
            .Setup(x => x.GetCityByOsmId(query.OsmId, query.OsmType))
            .ReturnsAsync(cityOSM);
        _cityDatabase
            .Setup(x => x.AddCity(It.IsAny<City>()))
            .Verifiable();
        
        // act
        var result = await _getCityByOsmIdHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType,
            Name = cityOSM.Name,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Population = cityOSM.Extratags?.Population ?? 0,
            Geojson = new Geojson
            {
                Type = cityOSM.Geojson?.Type ?? string.Empty,
                Coordinates = cityOSM.Geojson?.Coordinates ?? new List<List<List<double>>>()
            }
        },options => options
            .ComparingByMembers<City>()
            .ComparingByMembers<Geojson>());
    }
    
    [Theory, AutoData]
    public async Task Handle_GetCityByOsmIdQuery_ReturnsCityWithCityInDb(
        GetCityByOsmIdQuery query, CityOSM cityOSM)
    {
        // arrange
        var city = new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType,
            Name = cityOSM.Name,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Population = cityOSM.Extratags?.Population ?? 0,
            Geojson = cityOSM.Geojson
        };
        _osmCityFetcher
            .Setup(x => x.GetCityByOsmId(query.OsmId, query.OsmType))
            .ReturnsAsync(cityOSM);
        _cityDatabase
            .Setup(x => x.GetCityById(cityOSM.OsmId))
            .ReturnsAsync(city);
        
        // act
        var result = await _getCityByOsmIdHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType,
            Name = cityOSM.Name,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Population = cityOSM.Extratags?.Population ?? 0,
            Geojson = new Geojson
            {
                Type = cityOSM.Geojson?.Type ?? string.Empty,
                Coordinates = cityOSM.Geojson?.Coordinates ?? new List<List<List<double>>>()
            }
        },options => options
            .ComparingByMembers<City>()
            .ComparingByMembers<Geojson>());
    }
}