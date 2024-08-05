using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;
namespace UnitTests.Architecture;

public class LayerConventionTest
{
    private static readonly ArchUnitNET.Domain.Architecture Architecture = 
        new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();
    
    
    private readonly IObjectProvider<IType> _domainLayer = Types()
        .That()
        .ResideInNamespace("LittleConqueror.AppService.*", useRegularExpressions: true)
        .As("Application Layer");
    private readonly IObjectProvider<IType> _DrivenLayer = Types()
        .That()
        .ResideInNamespace("LittleConqueror.Infrastructure.*", useRegularExpressions: true)
        .As("Driven layer");
    private readonly IObjectProvider<IType> _DrivingLayer = Types()
        .That()
        .ResideInNamespace("LittleConqueror.API.*", useRegularExpressions: true)
        .As("Driving layer");
    
    [Fact]
    public void Types_that_resides_in_the_domain_layer_should_not_depend_on_any_driven_layer()
    {
        // arrange act assert
        Types()
            .That()
            .Are(_domainLayer)
            .Should()
            .NotDependOnAny(_DrivenLayer)
            .Because("Domain layer should not depend on any driven layer")
            .Check(Architecture);
    }
    
    [Fact]
    public void Types_that_resides_in_the_driving_layer_should_not_depend_on_any_driven_layer()
    {
        // arrange act assert
        Types()
            .That()
            .Are(_DrivingLayer)
            .Should()
            .NotDependOnAny(_DrivenLayer)
            .Because("Driving layer should not depend on any driven layer")
            .Check(Architecture);
    }
}