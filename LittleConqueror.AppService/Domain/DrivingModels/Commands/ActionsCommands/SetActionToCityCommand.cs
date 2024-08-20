namespace LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;


public class SetActionToCityCommand
{
    public long CityId { get; set; }
}

public enum ActionType
{
    Agricole,
    Miniere,
    Militaire,
    Espionnage,
    Diplomatique,
    Technologique
}