using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.API.Models.Dtos;

public record TechResearchDataDto
{
    public TechResearchType ResearchType { get; init; }
    public TechResearchCategory ResearchCategory { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Cost { get; init; }
    public List<TechResearchType> Prerequisites { get; init; }
    public TechResearchStatus ResearchStatus { get; init; } 
    public TechResearchAvailabilityEnum Availability { get; init; }
    public string? StartSearchingDate { get; init; }
    public string? EndSearchingDate { get; init; }
}