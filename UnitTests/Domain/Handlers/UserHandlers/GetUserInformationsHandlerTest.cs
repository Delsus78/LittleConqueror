using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers.UserHandlers;

public class GetUserInformationsHandlerTest
{
    private readonly Mock<IGetUserByIdHandler> _getUserByIdHandler;
    private readonly Mock<IGetTerritoryByUserIdHandler> _getTerritoryByUserIdHandler;
    private readonly Mock<IGetResourcesForUserHandler> _getResourcesForUserHandler;
    private readonly GetUserInformationsHandler _getUserInformationsHandler;
    
    public GetUserInformationsHandlerTest()
    {
        _getUserByIdHandler = new Mock<IGetUserByIdHandler>();
        _getTerritoryByUserIdHandler = new Mock<IGetTerritoryByUserIdHandler>();
        _getResourcesForUserHandler = new Mock<IGetResourcesForUserHandler>();

        _getUserInformationsHandler = new GetUserInformationsHandler(
            _getUserByIdHandler.Object,
            _getTerritoryByUserIdHandler.Object,
            _getResourcesForUserHandler.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserInformationsQuery_ReturnsUserInformations(
        GetUserInformationsQuery query)
    {
        // arrange
        var user = new User { Id = query.UserId, Name = "Test" };
        var territory = new Territory { OwnerId = user.Id };
        var resources = Resources.CreateDefaultResources(user.Id);
        _getUserByIdHandler
            .Setup(x => x.Handle(It.IsAny<GetUserByIdQuery>()))
            .ReturnsAsync(user);
        _getTerritoryByUserIdHandler
            .Setup(x => x.Handle(It.IsAny<GetTerritoryByUserIdQuery>()))
            .ReturnsAsync(territory);
        _getResourcesForUserHandler
            .Setup(x => x.Handle(It.IsAny<GetResourcesForUserQuery>()))
            .ReturnsAsync(resources);
        
        // act
        var result = await _getUserInformationsHandler.Handle(query);
        
        // assert
        result.Should().NotBeNull();
        result.Id.Should().Be(user.Id);
        result.Name.Should().Be(user.Name);
        if (query.IncludeTerritory)
        {
            result.TotalPopulation.Should().Be(territory.Cities.Sum(c => c.Population));
            result.TotalCities.Should().Be(territory.Cities.Count);
        }
        if (query.IncludeResources)
            result.Resources.Should().Be(resources);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserInformationsQuery_UserNotFound_ThrowsAppException(
        GetUserInformationsQuery query)
    {
        // arrange
        _getUserByIdHandler
            .Setup(x => x.Handle(It.IsAny<GetUserByIdQuery>()))
            .ThrowsAsync(new AppException("User not found", 404));
        
        // act
        Func<Task> act = async () => await _getUserInformationsHandler.Handle(query);
        
        // assert
        await act.Should().ThrowAsync<AppException>()
            .WithMessage("User not found");
    }
}