using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
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
    ITechDataFactoryService techDataFactoryService,
    ITechResearchConfigsProviderPort techResearchConfigsProvider) : IGetTechTreeOfUserIdHandler
{
    public async Task<List<TechResearchData>> Handle(GetTechTreeOfUserIdQuery query)
    {
        if (query.UserId != userContext.UserId)
            throw new AppException("You are not the owner of this tech tree", 403);
        
        var techResearches = await techResearchDatabase.GetAllTechResearchsForUser(query.UserId);
        
        return await TransformTechResearchesFromUserResearchList(techResearches);
    }
    
    private async Task<List<TechResearchData>> TransformTechResearchesFromUserResearchList(IEnumerable<TechResearch> techResearches)
    {
        var result = new List<TechResearchData>();
        var researches = techResearches.ToList();
        var techConfigs = await techResearchConfigsProvider.GetAll();
        
        result.AddRange(researches
            .Select(techResearch => techDataFactoryService
                .CreateTechResearchesDataAsync(techResearch).Result)
            .ToList());
        
        result.AddRange(techConfigs
            .Where(techConstants => researches.All(techResearch =>
                techResearch.ResearchType != techConstants.Type))
            .Select(techConstants => techDataFactoryService
                .CreateTechResearchesDataAsync(techConstants.Category, techConstants.Type,
                    TechResearchStatus.Undiscovered).Result)
            .ToList());
        
        return result;
    }
}