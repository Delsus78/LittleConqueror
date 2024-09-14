using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;
using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Technologique : Action
{
    public TechResearchCategories TechResearchCategory { get; set; }

    public int SciencePoints => TechnologiqueExpressions.GetResearchPointsProductionExpression(City.Population, 0.5);
}