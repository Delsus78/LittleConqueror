using AutoFixture.Xunit2;
using FluentAssertions;
using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Handlers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using Moq;
using Xunit;

namespace UnitTests.Domain.Handlers;

public class CreateUserHandlerTest
{
    private readonly Mock<IUserDatabasePort> _userDatabase;
    private readonly Mock<ITerritoryDatabasePort> _territoryDatabase;
    private readonly CreateUserHandler _createUserHandler;
    
    public CreateUserHandlerTest()
    {
        _userDatabase = new Mock<IUserDatabasePort>();
        _territoryDatabase = new Mock<ITerritoryDatabasePort>();
        
        _createUserHandler = new CreateUserHandler(_userDatabase.Object, _territoryDatabase.Object);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateUserCommand_ReturnsUser(CreateUserCommand command)
    {
        // arrange
        var expected = new User { Id = 1, Name = command.Name };
        _userDatabase
            .Setup(x => x.CreateUser(It.IsAny<User>()))
            .ReturnsAsync(expected);
        
        // act
        var result = await _createUserHandler.Handle(command);
        
        // assert
        result.Should().BeEquivalentTo(expected);
    }
    
    [Theory, AutoData]
    public async Task Handle_CreateUserCommand_ReturnsUserWithTerritory(CreateUserCommand command)
    {
        // arrange
        var expectedUser = new User { Id = 1, Name = command.Name };
        var expectedTerritory = new Territory { Id = 1, Owner = expectedUser };
        _userDatabase
            .Setup(x => x.CreateUser(It.IsAny<User>()))
            .ReturnsAsync(expectedUser);
        _territoryDatabase
            .Setup(x => x.CreateTerritory(It.IsAny<Territory>()))
            .ReturnsAsync(expectedTerritory);
        
        // act
        var result = await _createUserHandler.Handle(command);
        
        // assert
        result.Should().BeEquivalentTo(expectedUser);
        result.Territory.Should().BeEquivalentTo(expectedTerritory);
    }
}