using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Handlers.UserHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers.AuthHandlers;

public class RegisterAuthUserHandlerTest
{
    private readonly Mock<IPasswordHasherPort> _passwordHasherPort;
    private readonly Mock<IConsumeRegistrationLinkRelatedDataHandler> _getRegistrationLinkRelatedDataHandler;
    private readonly Mock<IAuthUserDatabasePort> _authUserDatabase;
    private readonly Mock<ICreateUserHandler> _createUserHandler;
    
    private readonly RegisterAuthUserHandler _registerAuthUserHandler;
    
    public RegisterAuthUserHandlerTest()
    {
        _passwordHasherPort = new Mock<IPasswordHasherPort>();
        _getRegistrationLinkRelatedDataHandler = new Mock<IConsumeRegistrationLinkRelatedDataHandler>();
        _authUserDatabase = new Mock<IAuthUserDatabasePort>();
        _createUserHandler = new Mock<ICreateUserHandler>();
        
        _registerAuthUserHandler = new RegisterAuthUserHandler(
            _passwordHasherPort.Object,
            _getRegistrationLinkRelatedDataHandler.Object,
            _authUserDatabase.Object,
            _createUserHandler.Object
        );
    }
    
    [Theory, AutoData]
    public async Task Handle_RegisterAuthUserCommand_ReturnsAuthUser(RegisterAuthUserCommand command)
    {
        // arrange
        var registrationLinkData = new RegistrationLinkData { Valid = true, Role = "Admin", FirstOsmId = 890, FirstOsmType = 'R'};
        _getRegistrationLinkRelatedDataHandler
            .Setup(x => x.Handle(It.IsAny<GetRegistrationLinkRelatedDataQuery>()))
            .Returns(registrationLinkData);
        
        var expectedUser = new User { Id = 1, Name = command.Username };
        _createUserHandler
            .Setup(x => x.Handle(It.IsAny<CreateUserCommand>()))
            .ReturnsAsync(expectedUser);
        
        var expectedAuthUser = new AuthUser { Id = 1, Username = command.Username, Role = registrationLinkData.Role, User = expectedUser };
        _authUserDatabase
            .Setup(x => x.AddAsync(It.IsAny<AuthUser>()))
            .ReturnsAsync(expectedAuthUser);
        
        // act
        var result = await _registerAuthUserHandler.Handle(command);
        
        // assert
        result.Should().BeEquivalentTo(expectedAuthUser);
    }
    
    [Theory, AutoData]
    public async Task Handle_InvalidRegistrationLink_ThrowsAppException(RegisterAuthUserCommand command)
    {
        // arrange
        var registrationLinkData = new RegistrationLinkData { Valid = false };
        _getRegistrationLinkRelatedDataHandler
            .Setup(x => x.Handle(It.IsAny<GetRegistrationLinkRelatedDataQuery>()))
            .Returns(registrationLinkData);
        
        // act
        Func<Task> act = async () => await _registerAuthUserHandler.Handle(command);
        
        // assert
        await act.Should().ThrowAsync<AppException>();
    }
}