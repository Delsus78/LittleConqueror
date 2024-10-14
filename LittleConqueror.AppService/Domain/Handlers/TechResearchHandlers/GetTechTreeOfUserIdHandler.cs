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

public class GetTechTreeOfUserIdHandler(IUserContext userContext, 
    ITechResearchDatabasePort techResearchDatabase,
    IGetPopulatedTechResearchesFromUserResearchListHandler getPopulatedTechResearchesFromUserResearchListHandler) : IGetTechTreeOfUserIdHandler
{
    public async Task<List<TechResearchData>> Handle(GetTechTreeOfUserIdQuery query)
    {
        if (userContext.IsUnauthorized(query.UserId))
            throw new AppException("You are not the owner of this tech tree", 403);
        
        var techResearches = await techResearchDatabase.GetAllTechResearchsForUser(query.UserId);

        return await getPopulatedTechResearchesFromUserResearchListHandler.Handle(
            new GetPopulatedTechResearchesFromUserResearchListQuery
            {
                TechResearches = techResearches,
                UserId = query.UserId
            });
    }
}