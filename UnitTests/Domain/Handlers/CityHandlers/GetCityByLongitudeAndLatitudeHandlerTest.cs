using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers.CityHandlers;

public class GetCityByLongitudeAndLatitudeHandlerTest
{
    private readonly Mock<IOSMCityFetcherPort> _osmCityFetcher;
    private readonly Mock<ICityDatabasePort> _cityDatabase;
    private readonly GetCityByLongitudeAndLatitudeHandler _getCityByLongitudeAndLatitudeHandler;
    
    public GetCityByLongitudeAndLatitudeHandlerTest()
    {
        _osmCityFetcher = new Mock<IOSMCityFetcherPort>();
        _cityDatabase = new Mock<ICityDatabasePort>();
        
        _getCityByLongitudeAndLatitudeHandler = new GetCityByLongitudeAndLatitudeHandler(_osmCityFetcher.Object, _cityDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetCityByLongitudeLatitudeQuery_ReturnsCityWithNoCityInDb(GetCityByLongitudeLatitudeQuery query)
    {
        // arrange
        var cityOSM = new CityOSM(
            1,
            'R',
            1,
            1,
            "City",
            new Extratags(1000),
            "",
            null);
        _osmCityFetcher
            .Setup(x => x.GetCityByLongitudeAndLatitude(query.Longitude, query.Latitude, 0))
            .ReturnsAsync(cityOSM);
        _cityDatabase
            .Setup(x => x.AddCity(It.IsAny<City>()))
            .Verifiable();
        
        // act
        var result = await _getCityByLongitudeAndLatitudeHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(new City
        {
            Id = cityOSM.OsmId,
            OsmType = cityOSM.OsmIdType,
            Name = cityOSM.Name,
            Latitude = cityOSM.Lat,
            Longitude = cityOSM.Lon,
            Population = cityOSM.Extratags?.Population ?? 0,
            Geojson = cityOSM.Geojson
        },options => options
            .ComparingByMembers<City>());
        
        _cityDatabase.Verify(x => x.AddCity(It.IsAny<City>()), Times.Once);
    }
}