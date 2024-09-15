using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Models.TechResearches;

public static class TechFactory
{
    public static TechResearch CreateTechResearch(DateTime researchDate, TechResearchCategory researchCategory, TechResearchType researchType, long userId)
    {
        return new TechResearch
        {
            ResearchDate = researchDate,
            ResearchCategory = researchCategory,
            ResearchType = researchType,
            UserId = userId
        };
    }
}

public static class TechDataFactory
{
    public static TechResearchData CreateTechResearches(
        TechResearchCategory researchCategory, 
        TechResearchType researchType,
        TechResearchStatus status)
    {
        var TechConstants = TechResearchesDataDictionaries.Values[researchType];
        return new TechResearchData
        {
            ResearchCategory = researchCategory,
            ResearchType = researchType,
            Cost = TechConstants.cost,
            Description = TechConstants.description,
            Name = TechConstants.name,
            Prerequisites = TechConstants.preReqs,
            ResearchStatus = status
        };
    }
}