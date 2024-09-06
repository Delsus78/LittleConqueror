using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface IGetTechTreeOfUserIdHandler
{
    Task<List<TechResearchData>> Handle(GetTechTreeOfUserIdQuery query);
}

public class GetTechTreeOfUserIdHandler(UserContext userContext, 
    ITechResearchDatabasePort techResearchDatabase,
    ITechRulesServices techRulesServices) : IGetTechTreeOfUserIdHandler
{
    public async Task<List<TechResearchData>> Handle(GetTechTreeOfUserIdQuery query)
    {
        if (query.UserId != userContext.UserId)
            throw new AppException("You are not the owner of this territory", 403);
        
        var techResearches = await techResearchDatabase.GetAllTechResearchsForUser(query.UserId);
        
        return techRulesServices.TransformTechResearchesFromUserResearchList(techResearches);
    }
}