using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.CityHandlers;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers.UserHandlers;

public class CreateUserHandlerTest
{
    private readonly Mock<IUserDatabasePort> _userDatabase;
    private readonly Mock<ICreateTerritoryHandler> _createTerritoryHandler;
    private readonly Mock<IAddCityToATerritoryHandler> _cityToATerritoryHandler;
    private readonly Mock<ICreateResourcesForUserHandler> _createResourcesForUserHandler;
    
    private readonly CreateUserHandler _createUserHandler;
    
    public CreateUserHandlerTest()
    {
        _userDatabase = new Mock<IUserDatabasePort>();
        _createTerritoryHandler = new Mock<ICreateTerritoryHandler>();
        _cityToATerritoryHandler = new Mock<IAddCityToATerritoryHandler>();
        _createResourcesForUserHandler = new Mock<ICreateResourcesForUserHandler>();
        var transactionManager = new Mock<ITransactionManagerPort>();
        
        _createUserHandler = new CreateUserHandler(
            _userDatabase.Object, 
            _createTerritoryHandler.Object,
            _cityToATerritoryHandler.Object,
            _createResourcesForUserHandler.Object,
            transactionManager.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateUserCommand_ReturnsUserWithTerritory(
        CreateUserCommand command)
    {
        // arrange
        var user = new User { Name = command.Name };
        var territory = new Territory { OwnerId = user.Id };
        _userDatabase.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync(user);
        _createTerritoryHandler.Setup(x => x.Handle(It.IsAny<CreateTerritoryCommand>())).ReturnsAsync(territory);
        
        // act
        var result = await _createUserHandler.Handle(command);
        
        // assert
        Assert.Equal(user, result);
        _userDatabase.Verify(x => x.CreateUser(It.IsAny<User>()), Times.Once);
        _createTerritoryHandler.Verify(x => x.Handle(It.IsAny<CreateTerritoryCommand>()), Times.Once);
        _cityToATerritoryHandler.Verify(x => x.Handle(It.IsAny<AddCityToATerritoryCommand>()), Times.Once);
        _createResourcesForUserHandler.Verify(x => x.Handle(It.IsAny<CreateResourcesForUserCommand>()), Times.Once);
    }
}