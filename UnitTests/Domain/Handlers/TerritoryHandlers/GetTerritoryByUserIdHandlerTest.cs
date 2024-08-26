using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.TerritoryHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers.TerritoryHandlers;

public class GetTerritoryByUserIdHandlerTest
{
    private readonly Mock<ITerritoryDatabasePort> _territoryDatabase;
    private readonly GetTerritoryByUserIdHandler _getTerritoryByUserIdHandler;
    private readonly Mock<IUserContext> _userContext;
    
    public GetTerritoryByUserIdHandlerTest()
    {
        _territoryDatabase = new Mock<ITerritoryDatabasePort>();
        _userContext = new Mock<IUserContext>();
        
        _getTerritoryByUserIdHandler = new GetTerritoryByUserIdHandler(_territoryDatabase.Object, _userContext.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserByIdQuery_ReturnUserWithTerritory(GetTerritoryByUserIdQuery query)
    {
        // arrange
        var owner = new User { Id = query.UserId, Name = "Test" };
        var expectedTerritory = new Territory { Id = 1, Owner = owner };
        owner.Territory = expectedTerritory;
        
        _territoryDatabase
            .Setup(x => x.GetTerritoryOfUser(It.IsAny<long>()))
            .ReturnsAsync(expectedTerritory);
        _userContext
            .Setup(x => x.UserId)
            .Returns(owner.Id);
        
        // act
        var result = await _getTerritoryByUserIdHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expectedTerritory);
        result.Owner.Should().BeEquivalentTo(owner);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserByIdQuery_ThrowsExceptionWhenTerritoryNotOwnedByUser(GetTerritoryByUserIdQuery query)
    {
        // arrange
        var expectedTerritory = new Territory { Id = 1, Owner = new User { Id = 1, Name = "Test2" } };
        
        _territoryDatabase
            .Setup(x => x.GetTerritoryOfUser(It.IsAny<long>()))
            .ReturnsAsync(expectedTerritory);
        _userContext
            .Setup(x => x.UserId)
            .Returns(-1);
        
        // act
        Func<Task> act = async () => await _getTerritoryByUserIdHandler.Handle(query);
        
        // assert
        await act.Should().ThrowAsync<AppException>();
    }
}