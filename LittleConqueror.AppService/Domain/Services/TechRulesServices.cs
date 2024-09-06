using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.Services;

public interface ITechRulesServices
{
    /// <summary>
    /// Transform a list of TechResearch entities to a list of TechResearchData
    /// </summary>
    /// <param name="techResearches">
    /// User's tech researches list, containing the researches that the user has already researched / is researching
    /// any research that the user don't have researched will be in the list with the status "NotResearched"
    /// </param>
    /// <returns></returns>
    List<TechResearchData> TransformTechResearchesFromUserResearchList(IEnumerable<TechResearch> techResearches);
}
public class TechRulesServices : ITechRulesServices
{
    public List<TechResearchData> TransformTechResearchesFromUserResearchList(IEnumerable<TechResearch> techResearches)
    {
        var result = new List<TechResearchData>();
        
        result.AddRange(techResearches
            .Select(techResearch => TechDataFactory
                .CreateTechResearches(techResearch.ResearchCategory, techResearch.ResearchType,
                    techResearch.ResearchStatus))
            .ToList());
        
        result.AddRange(TechResearchesDataDictionaries.Values
            .Where(techConstants => techResearches.All(techResearch =>
                techResearch.ResearchType != techConstants.Key))
            .Select(techConstants => TechDataFactory
                .CreateTechResearches(techConstants.Value.category, techConstants.Key,
                    TechResearchStatus.Undiscovered))
            .ToList());
        
        return result;
    }
}