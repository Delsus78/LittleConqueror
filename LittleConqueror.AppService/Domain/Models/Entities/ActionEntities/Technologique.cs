using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Technologique : Action
{
    public TechResearchCategories TechResearchCategory { get; set; }
}