using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.API.Models.Dtos;

public record TechResearchDataDto(
    TechResearchTypes ResearchType,
    TechResearchCategories ResearchCategory,
    string Name,
    string Description,
    int Cost,
    List<TechResearchTypes> Prerequisites,
    TechResearchStatus ResearchStatus
);