using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Models.TechResearches;

public interface ITechDataFactoryService
{
    Task<TechResearchData> CreateTechResearchesAsync(
        TechResearchCategory researchCategory,
        TechResearchType researchType,
        TechResearchStatus status);
}
public class TechDataFactoryService(ITechResearchConfigsProviderPort techResearchConfigsProvider) : ITechDataFactoryService
{
    public async Task<TechResearchData> CreateTechResearchesAsync(
        TechResearchCategory researchCategory, 
        TechResearchType researchType,
        TechResearchStatus status)
    {
        var TechConstants = await techResearchConfigsProvider.GetByType(researchType);
        return new TechResearchData
        {
            ResearchCategory = researchCategory,
            ResearchType = researchType,
            Cost = TechConstants.Cost,
            Description = TechConstants.Description,
            Name = TechConstants.Name,
            Prerequisites = TechConstants.PreReqs,
            ResearchStatus = status
        };
    }
}