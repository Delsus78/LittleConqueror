namespace LittleConqueror.AppService.Domain.Models.TechResearches.Configs;

public class TechConfig
{
    public TechResearchType Type { get; set; }
    public int Cost { get; set; }
    public TimeSpan ResearchTime { get; set; }
    public TechResearchCategory Category { get; set; }
    public List<TechResearchType> PreReqs { get; set; } = new();
    public required string Name { get; set; }
    public required string Description { get; set; }
}