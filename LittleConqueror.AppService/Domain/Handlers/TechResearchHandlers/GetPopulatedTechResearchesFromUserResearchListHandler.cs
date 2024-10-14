using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface IGetPopulatedTechResearchesFromUserResearchListHandler
{
    Task<List<TechResearchData>> Handle(GetPopulatedTechResearchesFromUserResearchListQuery query);
}
public class GetPopulatedTechResearchesFromUserResearchListHandler(
    ITechDataFactoryService techDataFactoryService,
    IGetSciencePointsOfUserIdHandler getSciencePointsOfUserIdHandler,
    ITechResearchConfigsDatabasePort techResearchConfigsDatabase) : IGetPopulatedTechResearchesFromUserResearchListHandler
{
    public async Task<List<TechResearchData>> Handle(GetPopulatedTechResearchesFromUserResearchListQuery query)
    {
        var userId = query.UserId;
        var techResearches = query.TechResearches;
        
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
            techResearch.Availabilities = new List<TechResearchAvailabilityEnum>();
            
            // ALREADY RESEARCHED CHECK
            if (techResearch.ResearchStatus is TechResearchStatus.Researched or TechResearchStatus.Researching)
                techResearch.Availabilities.Add(TechResearchAvailabilityEnum
                    .TechResearchAlreadyInProgressOrCompleted);
            
            // AN OTHER TECH RESEARCH IS ALREADY IN PROGRESS CHECK
            if (techResearchData.Any(data => data.ResearchStatus == TechResearchStatus.Researching))
                techResearch.Availabilities.Add(TechResearchAvailabilityEnum
                    .AnotherTechResearchIsAlreadyInProgress);
        
            // COST CHECK
            if (techResearch.Cost > sciencePoints[techResearch.ResearchCategory])
                techResearch.Availabilities.Add(TechResearchAvailabilityEnum
                    .NotEnoughSciencePoints);
        
            // PREREQUISITE TECH RESEARCH CHECK
            if (techResearchData
                .Any(data => techResearch.Prerequisites.Contains(data.ResearchType) && data.ResearchStatus != TechResearchStatus.Researched))
                techResearch.Availabilities.Add(TechResearchAvailabilityEnum
                    .PrerequisiteTechResearchNotCompleted);
            
            // AVAILABLE
            else 
                techResearch.Availabilities.Add(TechResearchAvailabilityEnum.Available);
        }
    }
}