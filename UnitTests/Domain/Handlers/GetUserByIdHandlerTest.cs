using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers;

public class GetUserByIdHandlerTest
{
    private readonly Mock<IUserDatabasePort> _userDatabase;
    private readonly GetUserByIdHandler _getUserByIdHandler;
    
    public GetUserByIdHandlerTest()
    {
        _userDatabase = new Mock<IUserDatabasePort>();
        
        _getUserByIdHandler = new GetUserByIdHandler(_userDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserByIdQuery_ReturnsUser(GetUserByIdQuery query)
    {
        // arrange
        var expected = new User { Id = query.UserId, Name = "Test" };
        _userDatabase
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _getUserByIdHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expected);
        result.Name.Should().Be("Test");
    }
    
    [Theory, AutoData]
    public async Task Handle_GetUserByIdQuery_ReturnsNull(GetUserByIdQuery query)
    {
        // arrange
        _userDatabase
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .ReturnsAsync((User?)null);
        
        // act
        var result = await _getUserByIdHandler.Handle(query);
        
        // assert
        result.Should().BeNull();
    }
}