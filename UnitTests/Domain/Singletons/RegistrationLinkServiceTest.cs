using LittleConqueror.AppService.Domain.Singletons;

namespace UnitTests.Domain.Singletons;

public class RegistrationLinkServiceTest
{
    private readonly RegistrationLinkService _registrationLinkService = new();


    [Theory, AutoData]
    public void CreateRegistrationLink_ReturnsLink(string role, int firstCityId)
    {
        // act
        var result = _registrationLinkService.CreateRegistrationLink(role, firstCityId);
        
        // assert
        result.Should().NotBeNullOrEmpty();
    }
    
    [Theory, AutoData]
    public void GetLinkRelatedData_ReturnsData(string role, int firstCityId)
    {
        // arrange
        var link = _registrationLinkService.CreateRegistrationLink(role, firstCityId);
        
        // act
        var result = _registrationLinkService.GetLinkRelatedData(link);
        
        // assert
        result.valid.Should().BeTrue();
        result.role.Should().Be(role);
        result.firstCardId.Should().Be(firstCityId);
    }
    
    [Theory, AutoData]
    public void GetLinkRelatedData_ReturnsInvalidData(string link)
    {
        // act
        var result = _registrationLinkService.GetLinkRelatedData(link);
        
        // assert
        result.valid.Should().BeFalse();
    }
}