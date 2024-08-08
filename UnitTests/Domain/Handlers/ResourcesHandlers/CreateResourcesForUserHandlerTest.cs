using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers.ResourcesHandlers;

public class CreateResourcesForUserHandlerTest
{
    private readonly Mock<IResourcesDatabasePort> _ressourcesDatabase;
    private readonly CreateResourcesForUserHandler _createResourcesForUserHandler;
    
    public CreateResourcesForUserHandlerTest()
    {
        _ressourcesDatabase = new Mock<IResourcesDatabasePort>();
        
        _createResourcesForUserHandler = new CreateResourcesForUserHandler(
            _ressourcesDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateResourcesForUserCommand_ReturnsResources(
        CreateResourcesForUserCommand command)
    {
        // arrange
        var resources = Resources.CreateDefaultResources(command.UserId);
        _ressourcesDatabase.Setup(x => x.CreateResources(It.IsAny<Resources>())).ReturnsAsync(resources);
        
        // act
        var result = await _createResourcesForUserHandler.Handle(command);
        
        // assert
        Assert.Equal(resources, result);
        _ressourcesDatabase.Verify(x => x.CreateResources(It.IsAny<Resources>()), Times.Once);
    }
}