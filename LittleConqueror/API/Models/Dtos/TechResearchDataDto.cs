using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.API.Models.Dtos;

public record TechResearchDataDto(
    TechResearchType ResearchType,
    TechResearchCategory ResearchCategory,
    string Name,
    string Description,
    int Cost,
    List<TechResearchType> Prerequisites,
    TechResearchStatus ResearchStatus,
    DateTime? StartSearchingDate,
    DateTime? EndSearchingDate
);