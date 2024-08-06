using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers;

public class GetUserInformationsHandlerTest
{
    private readonly Mock<IUserDatabasePort> _userDatabase;
    private readonly Mock<ITerritoryDatabasePort> _territoryDatabase;
    private readonly GetUserInformationsHandler _getUserInformationsHandler;
    
    public GetUserInformationsHandlerTest()
    {
        _userDatabase = new Mock<IUserDatabasePort>();
        _territoryDatabase = new Mock<ITerritoryDatabasePort>();
        
        _getUserInformationsHandler = new GetUserInformationsHandler(_userDatabase.Object, _territoryDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserInformationsQuery_ReturnsUserInformations(GetUserInformationsQuery query)
    {
        // arrange
        var user = new User { Id = query.UserId, Name = "User" };
        var territory = new Territory
        {
            Id = 1,
            Owner = user,
            Cities = new List<City>
            {
                new() { Id = 1, Name = "City1", Population = 100 },
                new() { Id = 2, Name = "City2", Population = 200 }
            }
        };
        _userDatabase
            .Setup(x => x.GetUserById(query.UserId))
            .ReturnsAsync(user);
        _territoryDatabase
            .Setup(x => x.GetTerritoryOfUser(query.UserId))
            .ReturnsAsync(territory);
        
        // act
        var result = await _getUserInformationsHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(new UserInformations
        {
            Id = user.Id,
            Name = user.Name,
            TotalPopulation = 300,
            TotalCities = 2
        });
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserInformationsQuery_UserNotFound_ThrowsAppException(GetUserInformationsQuery query)
    {
        // arrange
        _userDatabase
            .Setup(x => x.GetUserById(query.UserId))
            .ReturnsAsync((User?)null);
        
        // act
        Func<Task> act = async () => await _getUserInformationsHandler.Handle(query);
        
        // assert
        await act.Should().ThrowAsync<AppException>()
            .WithMessage("User not found");
    }
}