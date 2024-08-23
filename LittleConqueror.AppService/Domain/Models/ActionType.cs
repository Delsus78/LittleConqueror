using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.AppService.Domain.Models;

public enum ActionType
{
    Agricole,
    Miniere,
    Militaire,
    Espionnage,
    Diplomatique,
    Technologique
}

public static class ActionTypeExtensions
{
    public static ActionType GetActionType(this ActionEntities.Action action)
    {
        return action switch
        {
            ActionEntities.Agricole => ActionType.Agricole,
            ActionEntities.Miniere => ActionType.Miniere,
            ActionEntities.Militaire => ActionType.Militaire,
            ActionEntities.Espionnage => ActionType.Espionnage,
            ActionEntities.Diplomatique => ActionType.Diplomatique,
            ActionEntities.Technologique => ActionType.Technologique,
            _ => throw new ArgumentOutOfRangeException(nameof(action))
        };
    }
}