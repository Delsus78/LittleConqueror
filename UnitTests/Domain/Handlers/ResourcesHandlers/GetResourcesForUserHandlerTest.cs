using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers.ResourcesHandlers;

public class GetResourcesForUserHandlerTest
{
    private readonly Mock<IResourcesDatabasePort> _resourceDatabaseMock;
    private readonly GetResourcesForUserHandler _getResourcesForUserHandler;
    
    public GetResourcesForUserHandlerTest()
    {
        _resourceDatabaseMock = new Mock<IResourcesDatabasePort>();
        
        _getResourcesForUserHandler = new GetResourcesForUserHandler(_resourceDatabaseMock.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetResourcesForUserQuery_ReturnsResources(GetResourcesForUserQuery query)
    {
        // arrange
        var expected = Resources.CreateDefaultResources(query.UserId);
        _resourceDatabaseMock
            .Setup(x => x.GetResourcesOfUser(query.UserId))
            .ReturnsAsync(expected);
        
        // act
        var result = await _getResourcesForUserHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetResourcesForUserQuery_ThrowsAppException(GetResourcesForUserQuery query)
    {
        // arrange
        _resourceDatabaseMock
            .Setup(x => x.GetResourcesOfUser(query.UserId))
            .ReturnsAsync((Resources)null);
        
        // act
        Func<Task> act = async () => await _getResourcesForUserHandler.Handle(query);
        
        // assert
        await act.Should().ThrowAsync<AppException>();
    }
}