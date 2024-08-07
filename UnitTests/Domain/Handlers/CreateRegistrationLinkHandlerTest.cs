using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Singletons;

namespace UnitTests.Domain.Handlers;

public class CreateRegistrationLinkHandlerTest
{
    private readonly CreateRegistrationLinkHandler _createRegistrationLinkHandler;
    private readonly Mock<IRegistrationLinkService> _userDatabase;
    
    public CreateRegistrationLinkHandlerTest()
    {
        _userDatabase = new Mock<IRegistrationLinkService>();
        _createRegistrationLinkHandler = new CreateRegistrationLinkHandler(_userDatabase.Object);
    }
    
    [Theory, AutoData]
    public void Handle_CreateRegistrationLinkCommand_ReturnsLink(CreateRegistrationLinkCommand command)
    {
        // arrange
        _userDatabase
            .Setup(x => x.CreateRegistrationLink(It.IsAny<RegistrationLinkData>()))
            .Returns("link");
        
        // act
        var result = _createRegistrationLinkHandler.Handle(command);
        
        // assert
        result.Should().NotBeNullOrEmpty();
    }
}