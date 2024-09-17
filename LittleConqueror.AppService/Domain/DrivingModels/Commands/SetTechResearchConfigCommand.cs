using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class SetTechResearchConfigCommand
{
    public required IEnumerable<TechConfig> TechConfigs { get; set; }
}