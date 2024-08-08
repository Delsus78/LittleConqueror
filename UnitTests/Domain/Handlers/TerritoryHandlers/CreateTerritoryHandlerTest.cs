using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers.TerritoryHandlers;

public class CreateTerritoryHandlerTest
{
    private readonly Mock<ITerritoryDatabasePort> _territoryDatabase;
    
    private readonly CreateTerritoryHandler _createTerritoryHandler;
    
    public CreateTerritoryHandlerTest()
    {
        _territoryDatabase = new Mock<ITerritoryDatabasePort>();
        _createTerritoryHandler = new CreateTerritoryHandler(_territoryDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateTerritoryCommand_ReturnsTerritory(CreateTerritoryCommand command)
    {
        // arrange
        var expected = new Territory { Id = 1, OwnerId = command.OwnerId };
        _territoryDatabase
            .Setup(x => x.CreateTerritory(It.IsAny<Territory>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _createTerritoryHandler.Handle(command);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateTerritoryCommand_ThrowsAppException(CreateTerritoryCommand command)
    {
        // arrange
        _territoryDatabase
            .Setup(x => x.CreateTerritory(It.IsAny<Territory>()))
            .ThrowsAsync(new AppException("Territory already exists", 400));
        
        // act
        Func<Task> act = async () => await _createTerritoryHandler.Handle(command);
        
        // assert
        await act.Should().ThrowAsync<AppException>();
    }
}