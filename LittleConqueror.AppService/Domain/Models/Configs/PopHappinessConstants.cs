namespace LittleConqueror.AppService.Domain.Models.Configs;

public enum PopHappinessType
{
    Food,
    Iron
}
public static class PopHappinessinititialisator
{
    public static Dictionary<PopHappinessType, double> Values { get; } = new() {
        {PopHappinessType.Food, 1},
        {PopHappinessType.Iron, 1}
    };
}