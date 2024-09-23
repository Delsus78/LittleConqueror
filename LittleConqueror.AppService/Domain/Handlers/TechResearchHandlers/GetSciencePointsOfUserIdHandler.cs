using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface IGetSciencePointsOfUserIdHandler
{
    Task<Dictionary<TechResearchCategory, int>> Handle(GetSciencePointsOfUserIdQuery query);
}

public class GetSciencePointsOfUserIdHandler(
    IActionDatabasePort actionDatabase, 
    IUserContext userContext) : IGetSciencePointsOfUserIdHandler
{
    public async Task<Dictionary<TechResearchCategory, int>> Handle(GetSciencePointsOfUserIdQuery query)
    {
        if (userContext.IsUnauthorized(query.UserId))
            throw new AppException("You are not authorized to access this resource", 403);
        
        var result = new Dictionary<TechResearchCategory, int>();
        
        foreach (var category in Enum.GetValues<TechResearchCategory>())
        {
            var points = await actionDatabase.ComputeTotalResearch(query.UserId, category);
            result.Add(category, points);
        }
        
        return result;
    }
}