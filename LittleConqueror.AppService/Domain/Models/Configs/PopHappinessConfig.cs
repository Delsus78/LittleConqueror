using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Models.Configs;

public class PopHappinessConfig
{
    public ResourceType Type { get; set; }
    public double Coef { get; set; }
}