using AutoFixture.Xunit2;
using FluentAssertions;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using Moq;
using Xunit;

namespace UnitTests.Domain.Handlers;

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
            1,
            1,
            null,
            "City",
            "CityName",
            new Address("City", "State", "Country"),
            new List<double>(),
            new Extratags(1000),
            null);
        _osmCityFetcher
            .Setup(x => x.GetCityByLongitudeAndLatitude(query.Longitude, query.Latitude))
            .ReturnsAsync(cityOSM);
        _cityDatabase
            .Setup(x => x.AddCity(It.IsAny<City>()))
            .Verifiable();
        
        // act
        var result = await _getCityByLongitudeAndLatitudeHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(new City
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
        });
        
        _cityDatabase.Verify(x => x.AddCity(It.IsAny<City>()), Times.Once);
    }
}