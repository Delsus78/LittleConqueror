namespace LittleConqueror.AppService.Domain.Models.TechResearches;

public class TechResearchData
{
    public TechResearchType ResearchType { get; init; }
    public TechResearchCategory ResearchCategory { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Cost { get; init; }
    public List<TechResearchType> Prerequisites { get; init; }
    public TechResearchStatus ResearchStatus { get; init; }
    
    public DateTime? StartSearchingDate { get; init; }
    public DateTime? EndSearchingDate { get; init; }
}