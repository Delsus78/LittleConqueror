using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;
using LittleConqueror.AppService.Domain.Models.Configs;

namespace LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

public class Technologique : Action
{
    public TechResearchCategory TechResearchCategory { get; set; }

    public int SciencePoints => TechnologiqueExpressions.GetResearchPointsProductionExpression(City.Population, 0.5);
}