namespace LittleConqueror.AppService.Domain.Models.TechResearches;

public class TechResearchData
{
    public TechResearchTypes ResearchType { get; init; }
    public TechResearchCategories ResearchCategory { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Cost { get; init; }
    public List<TechResearchTypes> Prerequisites { get; init; }
    public TechResearchStatus ResearchStatus { get; init; }
}