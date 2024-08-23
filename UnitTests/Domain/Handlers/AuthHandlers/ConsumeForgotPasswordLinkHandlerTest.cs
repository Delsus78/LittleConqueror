using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Services;

namespace UnitTests.Domain.Handlers.AuthHandlers;

public class ConsumeForgotPasswordLinkHandlerTest
{
    private readonly Mock<ITemporaryCodeService> _registrationLinkService;
    private readonly ConsumeForgotPasswordLinkRelatedDataHandler _consumeForgotPasswordLinkRelatedDataHandler;
    
    public ConsumeForgotPasswordLinkHandlerTest()
    {
        _registrationLinkService = new Mock<ITemporaryCodeService>();
        
        _consumeForgotPasswordLinkRelatedDataHandler = new ConsumeForgotPasswordLinkRelatedDataHandler(_registrationLinkService.Object);
    }
    
    [Theory, AutoData]
    public void Handle_ConsumeForgotPasswordLinkRelatedDataQuery_ReturnsLinkRelatedData(GetForgotPasswordLinkRelatedDataQuery query)
    {
        // arrange
        var expected = new ForgetPasswordLinkData { Valid = true, Username = "Admin" };
        _registrationLinkService
            .Setup(x => x.ConsumeLinkRelatedData<ForgetPasswordLinkData>(query.Link))
            .Returns(expected);
        
        // act
        var result = _consumeForgotPasswordLinkRelatedDataHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
}