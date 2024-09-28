using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Configs;
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
    IGetSciencePointsOfUserIdHandler getSciencePointsOfUserIdHandler,
    ITechResearchConfigsDatabasePort techResearchConfigsDatabase) : IGetTechTreeOfUserIdHandler
{
    public async Task<List<TechResearchData>> Handle(GetTechTreeOfUserIdQuery query)
    {
        if (userContext.IsUnauthorized(query.UserId))
            throw new AppException("You are not the owner of this tech tree", 403);
        
        var techResearches = await techResearchDatabase.GetAllTechResearchsForUser(query.UserId);
        
        return await TransformTechResearchesFromUserResearchList(techResearches, query.UserId);
    }
    
    private async Task<List<TechResearchData>> TransformTechResearchesFromUserResearchList(IEnumerable<TechResearch> techResearches, long userId)
    {
        var result = new List<TechResearchData>();
        var researches = techResearches.ToList();
        var techConfigs = await techResearchConfigsDatabase.GetAllTechConfigs();
        
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
        
        // Populate availability
        var sciencePoints = await getSciencePointsOfUserIdHandler
            .Handle(new GetSciencePointsOfUserIdQuery { UserId = userId });

        PopulateTechResearchDataWithAvailability(result, sciencePoints);
        
        return result;
    }
    
    private static void PopulateTechResearchDataWithAvailability(List<TechResearchData> techResearchData, IReadOnlyDictionary<TechResearchCategory, int> sciencePoints)
    {
        foreach (var techResearch in techResearchData)
        {
            // ALREADY RESEARCHED CHECK
            if (techResearch.ResearchStatus is TechResearchStatus.Researched or TechResearchStatus.Researching)
                techResearch.Availability = TechResearchAvailabilityEnum
                    .TechResearchAlreadyInProgressOrCompleted;
            
            // AN OTHER TECH RESEARCH IS ALREADY IN PROGRESS CHECK
            else if (techResearchData.Any(data => data.ResearchStatus == TechResearchStatus.Researching))
                techResearch.Availability = TechResearchAvailabilityEnum
                    .AnotherTechResearchIsAlreadyInProgress;
        
            // COST CHECK
            else if (techResearch.Cost > sciencePoints[techResearch.ResearchCategory])
                techResearch.Availability = TechResearchAvailabilityEnum
                    .NotEnoughSciencePoints;
        
            // PREREQUISITE TECH RESEARCH CHECK
            else if (techResearchData
                .Any(data => techResearch.Prerequisites.Contains(data.ResearchType) && data.ResearchStatus != TechResearchStatus.Researched))
                techResearch.Availability = TechResearchAvailabilityEnum
                    .PrerequisiteTechResearchNotCompleted;
            
            // AVAILABLE
            else 
                techResearch.Availability = TechResearchAvailabilityEnum.Available;
        }
    }
}