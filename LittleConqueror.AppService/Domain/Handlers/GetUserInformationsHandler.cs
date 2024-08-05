using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers;

public interface IGetUserInformationsHandler
{
    Task<UserInformations?> Handle(GetUserInformationsQuery query);
}
public class GetUserInformationsHandler(
    IUserDatabasePort userDatabase,
    ITerritoryDatabasePort territoryDatabase) : IGetUserInformationsHandler
{
    public async Task<UserInformations?> Handle(GetUserInformationsQuery query)
    {
        var user = await userDatabase.GetUserById(query.UserId);
        if (user == null) 
            throw new AppException("User not found", 404);
        
        var territory = await territoryDatabase.GetTerritoryOfUser(query.UserId);
        
        return new UserInformations
        {
            Id = user.Id,
            Name = user.Name,
            TotalPopulation = territory.Cities.Sum(c => c.Population),
            TotalCities = territory.Cities.Count()
        };
    }
}