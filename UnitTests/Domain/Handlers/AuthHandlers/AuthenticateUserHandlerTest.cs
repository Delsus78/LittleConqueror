using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace UnitTests.Domain.Handlers.AuthHandlers;

public class AuthenticateUserHandlerTest
{
    private readonly Mock<IAuthUserDatabasePort> _authUserDatabase;
    private readonly Mock<IPasswordHasherPort> _passwordHasher;
    private readonly Mock<IJwtTokenProviderPort> _jwtTokenProvider;
    private readonly AuthenticateUserHandler _authenticateUserHandler;
    
    public AuthenticateUserHandlerTest()
    {
        _authUserDatabase = new Mock<IAuthUserDatabasePort>();
        _passwordHasher = new Mock<IPasswordHasherPort>();
        _jwtTokenProvider = new Mock<IJwtTokenProviderPort>();
        
        _authenticateUserHandler = new AuthenticateUserHandler(_authUserDatabase.Object, _passwordHasher.Object, _jwtTokenProvider.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_AuthenticateUserCommand_ReturnsAuthUserAndToken(AuthenticateUserCommand command)
    {
        // arrange
        var user = new AuthUser { Id = 1, Username = command.Username, Hash = "hash" };
        _authUserDatabase
            .Setup(x => x.GetAuthUserByUsername(It.IsAny<string>()))
            .ReturnsAsync(user);
        _passwordHasher
            .Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
        _jwtTokenProvider
            .Setup(x => x.GenerateJwtToken(It.IsAny<AuthUser>()))
            .ReturnsAsync("token");
        
        // act
        var result = await _authenticateUserHandler.Handle(command);
        
        // assert
        result.authUser.Should().BeEquivalentTo(user);
        result.token.Should().Be("token");
    }
    
    [Theory, AutoData]
    public async Task Handle_AuthenticateUserCommand_ThrowsAppException(AuthenticateUserCommand command)
    {
        // arrange
        _authUserDatabase
            .Setup(x => x.GetAuthUserByUsername(It.IsAny<string>()))
            .ReturnsAsync((AuthUser)null);
        _passwordHasher
            .Setup(x => x.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);
        
        // act
        Func<Task> act = async () => await _authenticateUserHandler.Handle(command);
        
        // assert
        await act.Should().ThrowAsync<AppException>()
            .WithMessage("Username or password is incorrect");
    }
}