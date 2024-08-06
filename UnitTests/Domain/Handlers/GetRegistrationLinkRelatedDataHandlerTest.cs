using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Singletons;

namespace UnitTests.Domain.Handlers;

public class GetRegistrationLinkRelatedDataHandlerTest
{
    private readonly Mock<IRegistrationLinkService> _registrationLinkService;
    private readonly GetRegistrationLinkRelatedDataHandler _getRegistrationLinkRelatedDataHandler;
    
    public GetRegistrationLinkRelatedDataHandlerTest()
    {
        _registrationLinkService = new Mock<IRegistrationLinkService>();
        
        _getRegistrationLinkRelatedDataHandler = new GetRegistrationLinkRelatedDataHandler(_registrationLinkService.Object);
    }
    
    [Theory, AutoData]
    public void Handle_GetRegistrationLinkRelatedDataQuery_ReturnsLinkRelatedData(GetRegistrationLinkRelatedDataQuery query)
    {
        // arrange
        var expected = (true, "role", 1);
        _registrationLinkService
            .Setup(x => x.GetLinkRelatedData(It.IsAny<string>()))
            .Returns(expected);
        
        // act
        var result = _getRegistrationLinkRelatedDataHandler.Handle(query);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
}