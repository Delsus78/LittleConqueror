using System.Linq.Expressions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class MilitaireExpressions
{
    public static Expression<Func<ActionEntities.Militaire, int>> GetFoodPriceExpression()
    {
        return actionMilitaire => actionMilitaire.City.Population;
    }
}