using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers;

public class CreateUserHandlerTest
{
    private readonly Mock<IUserDatabasePort> _userDatabase;
    private readonly Mock<ITerritoryDatabasePort> _territoryDatabase;
    private readonly Mock<IAddCityToATerritoryHandler> _cityToATerritoryHandler;
    private readonly CreateUserHandler _createUserHandler;
    
    public CreateUserHandlerTest()
    {
        _userDatabase = new Mock<IUserDatabasePort>();
        _territoryDatabase = new Mock<ITerritoryDatabasePort>();
        _cityToATerritoryHandler = new Mock<IAddCityToATerritoryHandler>();
        var transactionManager = new Mock<ITransactionManagerPort>();
        
        _createUserHandler = new CreateUserHandler(
            _userDatabase.Object, 
            _territoryDatabase.Object,
            _cityToATerritoryHandler.Object,
            transactionManager.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateUserCommand_ReturnsUser(CreateUserCommand command)
    {
        // arrange
        var expected = new User { Id = 1, Name = command.Name };
        _userDatabase
            .Setup(x => x.CreateUser(It.IsAny<User>()))
            .ReturnsAsync(expected);
        
        _territoryDatabase
            .Setup(x => x.CreateTerritory(It.IsAny<Territory>()))
            .ReturnsAsync((Territory t) => t);
        
        _cityToATerritoryHandler
            .Setup(x => x.Handle(It.IsAny<AddCityToATerritoryCommand>()));
        
        // act
        var result = await _createUserHandler.Handle(command);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
}