using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace UnitTests.Domain.Handlers.AuthHandlers;

public class ChangePasswordHandlerTest
{
    private readonly Mock<IPasswordHasherPort> _passwordHasher;
    private readonly Mock<IAuthUserDatabasePort> _authUserDatabase;
    private readonly Mock<IConsumeForgotPasswordLinkRelatedDataHandler> _getForgotPasswordLinkRelatedDataHandler;
    private readonly ChangePasswordHandler _changePasswordHandler;

    public ChangePasswordHandlerTest()
    {
        _passwordHasher = new Mock<IPasswordHasherPort>();
        _authUserDatabase = new Mock<IAuthUserDatabasePort>();
        _getForgotPasswordLinkRelatedDataHandler = new Mock<IConsumeForgotPasswordLinkRelatedDataHandler>();

        _changePasswordHandler = new ChangePasswordHandler(
            _passwordHasher.Object,
            _getForgotPasswordLinkRelatedDataHandler.Object,
            _authUserDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_ChangePasswordCommand_ReturnsLink(ChangePasswordCommand command)
    {
        // arrange
        var linkData = new ForgetPasswordLinkData { Valid = true, Username = "Admin" };
        _getForgotPasswordLinkRelatedDataHandler
            .Setup(x => x.Handle(It.IsAny<GetForgotPasswordLinkRelatedDataQuery>()))
            .Returns(linkData);
        
        var authUser = new AuthUser { Username = "Admin" };
        _authUserDatabase
            .Setup(x => x.GetAuthUserByUsername("Admin"))
            .ReturnsAsync(authUser);
        
        _passwordHasher
            .Setup(x => x.EnhancedHashPassword(command.Password))
            .Returns("hashedPassword");
        
        // act
        await _changePasswordHandler.Handle(command);
        
        // assert
        _authUserDatabase.Verify(x => x.UpdateAsync(It.Is<AuthUser>(x => x.Hash == "hashedPassword")), Times.Once);
    }
}