using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.AuthHandlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Services;

namespace UnitTests.Domain.Handlers.AuthHandlers;

public class GetRegistrationLinkRelatedDataHandlerTest
{
    private readonly Mock<ITemporaryCodeService> _registrationLinkService;
    private readonly ConsumeRegistrationLinkRelatedDataHandler _consumeRegistrationLinkRelatedDataHandler;
    
    public GetRegistrationLinkRelatedDataHandlerTest()
    {
        _registrationLinkService = new Mock<ITemporaryCodeService>();
        
        _consumeRegistrationLinkRelatedDataHandler = new ConsumeRegistrationLinkRelatedDataHandler(_registrationLinkService.Object);
    }
    
    [Theory, AutoData]
    public void Handle_GetRegistrationLinkRelatedDataQuery_ReturnsLinkRelatedData(GetRegistrationLinkRelatedDataQuery query)
    {
        // arrange
        var expected = new RegistrationLinkData { Valid = true, Role = "Admin", FirstOsmId = 'R' };
        _registrationLinkService
            .Setup(x => x.ConsumeLinkRelatedData<RegistrationLinkData>(query.Link))
            .Returns(expected);
        
        // act
        var result = _consumeRegistrationLinkRelatedDataHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
}