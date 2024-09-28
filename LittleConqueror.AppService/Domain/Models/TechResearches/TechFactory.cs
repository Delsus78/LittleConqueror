using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Models.TechResearches;

public interface ITechDataFactoryService
{
    Task<TechResearchData> CreateTechResearchesDataAsync(
        TechResearchCategory researchCategory,
        TechResearchType researchType,
        TechResearchStatus status);
    
    Task<TechResearchData> CreateTechResearchesDataAsync(TechResearch techResearch);
}
public class TechDataFactoryService(ITechResearchConfigsDatabasePort techResearchConfigsDatabase) : ITechDataFactoryService
{
    public async Task<TechResearchData> CreateTechResearchesDataAsync(
        TechResearchCategory researchCategory, 
        TechResearchType researchType,
        TechResearchStatus status)
    {
        var TechConstants = await techResearchConfigsDatabase.GetTechConfigByType(researchType);
        return new TechResearchData
        {
            ResearchCategory = researchCategory,
            ResearchType = researchType,
            Cost = TechConstants.Cost,
            Description = TechConstants.Description,
            Name = TechConstants.Name,
            Prerequisites = TechConstants.PreReqs,
            ResearchStatus = status,
            StartSearchingDate = DateTime.Now.ToUniversalTime(),
            EndSearchingDate = DateTime.Now.ToUniversalTime().Add(TechConstants.ResearchTime)
        };
    }

    public async Task<TechResearchData> CreateTechResearchesDataAsync(TechResearch techResearch)
    {
        var TechConstants = await techResearchConfigsDatabase.GetTechConfigByType(techResearch.ResearchType);
        return new TechResearchData
        {
            ResearchCategory = techResearch.ResearchCategory,
            ResearchType = techResearch.ResearchType,
            Cost = TechConstants.Cost,
            Description = TechConstants.Description,
            Name = TechConstants.Name,
            Prerequisites = TechConstants.PreReqs,
            ResearchStatus = techResearch.ResearchStatus,
            StartSearchingDate = techResearch.ResearchDate,
            EndSearchingDate = techResearch.ResearchDate?.Add(TechConstants.ResearchTime)
        };
    }
}