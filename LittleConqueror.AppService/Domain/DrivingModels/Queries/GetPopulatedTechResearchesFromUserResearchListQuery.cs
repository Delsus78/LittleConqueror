using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetPopulatedTechResearchesFromUserResearchListQuery
{
    public required IEnumerable<TechResearch> TechResearches { get; init; } 
    public required long UserId { get; init; }
}