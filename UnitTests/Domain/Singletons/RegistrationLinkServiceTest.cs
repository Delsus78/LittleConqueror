using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Singletons;

namespace UnitTests.Domain.Singletons;

public class RegistrationLinkServiceTest
{
    private readonly TemporaryCodeService _temporaryCodeService = new();


    [Theory, AutoData]
    public void CreateRegistrationLink_ReturnsLink(RegistrationLinkData data)
    {
        // act
        var result = _temporaryCodeService.CreateRegistrationLink(data);
        
        // assert
        result.Should().NotBeNullOrEmpty();
    }
    
    [Theory, AutoData]
    public void GetLinkRelatedData_ReturnsData(RegistrationLinkData data)
    {
        // arrange
        var link = _temporaryCodeService.CreateRegistrationLink(data);
        
        // act
        var result = _temporaryCodeService.ConsumeLinkRelatedData<RegistrationLinkData>(link);
        
        // assert
        result.Valid.Should().BeTrue();
        result.Role.Should().Be(data.Role);
        result.FirstOsmId.Should().Be(data.FirstOsmId);
    }
    
    [Theory, AutoData]
    public void GetLinkRelatedData_ReturnsInvalidData(string link)
    {
        // act
        var result = _temporaryCodeService.ConsumeLinkRelatedData<RegistrationLinkData>(link);
        
        // assert
        result.Valid.Should().BeFalse();
    }
}