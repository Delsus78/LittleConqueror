using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers;

public class GetAuthenticatedUserByIdHandlerTest
{
    private readonly Mock<IAuthUserDatabasePort> _authUserDatabase;
    private readonly GetAuthenticatedUserByIdHandler _getAuthenticatedUserByIdHandler;
    
    public GetAuthenticatedUserByIdHandlerTest()
    {
        _authUserDatabase = new Mock<IAuthUserDatabasePort>();
        
        _getAuthenticatedUserByIdHandler = new GetAuthenticatedUserByIdHandler(_authUserDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetAuthenticatedUserByIdQuery_ReturnsAuthUser(GetAuthenticatedUserByIdQuery query)
    {
        // arrange
        var expected = new AuthUser { Id = query.UserId, Username = "testUser" };
        _authUserDatabase
            .Setup(x => x.GetAuthUserById(query.UserId))
            .ReturnsAsync(expected);
        
        // act
        var result = await _getAuthenticatedUserByIdHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory, AutoData]
    public async Task Handle_GetAuthenticatedUserByIdQuery_ThrowsAppException(GetAuthenticatedUserByIdQuery query)
    {
        // arrange
        _authUserDatabase
            .Setup(x => x.GetAuthUserById(query.UserId))
            .ReturnsAsync((AuthUser)null);
        
        // act
        Func<Task> act = async () => await _getAuthenticatedUserByIdHandler.Handle(query);
        
        // assert
        await act.Should().ThrowAsync<AppException>();
    }
}