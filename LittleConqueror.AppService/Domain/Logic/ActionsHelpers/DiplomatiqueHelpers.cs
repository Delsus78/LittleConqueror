using System.Linq.Expressions;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Logic.ActionsHelpers;

public static class DiplomatiqueExpressions
{
    public static Expression<Func<ActionEntities.Diplomatique, int>> GetFoodPriceExpression()
    {
        return actionDiplomatique => actionDiplomatique.City.Population;
    }
}