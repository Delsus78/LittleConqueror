using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers;

public class AddCityToATerritoryHandlerTest
{
    private readonly Mock<ITerritoryDatabasePort> _territoryDatabaseMock;
    private readonly Mock<ICityDatabasePort> _cityDatabaseMock;
    private readonly Mock<IGetCityByOsmIdHandler> _getCityByOsmIdHandlerMock;
    
    private readonly AddCityToATerritoryHandler _addCityToATerritoryHandler;

    public AddCityToATerritoryHandlerTest()
    {
        _territoryDatabaseMock = new Mock<ITerritoryDatabasePort>();
        _cityDatabaseMock = new Mock<ICityDatabasePort>();
        _getCityByOsmIdHandlerMock = new Mock<IGetCityByOsmIdHandler>();
        
        _addCityToATerritoryHandler = new AddCityToATerritoryHandler(
            _territoryDatabaseMock.Object, 
            _cityDatabaseMock.Object, 
            _getCityByOsmIdHandlerMock.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_AddCityToTerritory_Successfully(AddCityToATerritoryCommand command)
    {
        // arrange
        var city = new City { Id = 1, OsmType = 'R'};
        var territory = new Territory { Id = 1 };
        _getCityByOsmIdHandlerMock
            .Setup(handler => handler.Handle(It.IsAny<GetCityByOsmIdQuery>()))
            .ReturnsAsync(city);
        
        _territoryDatabaseMock
            .Setup(database => database.GetTerritoryById(It.IsAny<int>()))
            .ReturnsAsync(territory);
        
        // act
        await _addCityToATerritoryHandler.Handle(command);
        
        // assert
        _cityDatabaseMock.Verify(database => database.SetTerritoryId(city.Id, territory.Id), Times.Once);
    }
    
    [Theory, AutoData]
    public async Task Handle_AddCityToTerritory_ThrowsAppException(
        AddCityToATerritoryCommand command)
    {
        // arrange
        var city = new City { Id = 1, OsmType = 'R'};
        _getCityByOsmIdHandlerMock
            .Setup(handler => handler.Handle(It.IsAny<GetCityByOsmIdQuery>()))
            .ReturnsAsync(city);
        
        _territoryDatabaseMock
            .Setup(database => database.GetTerritoryById(It.IsAny<int>()))
            .ReturnsAsync((Territory)null);
        
        // act & assert
        await Assert.ThrowsAsync<AppException>(() => _addCityToATerritoryHandler.Handle(command));
    }
}