using LittleConqueror.AppService.Domain.Models.Entities.Base;
using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class Configs : Entity
{
    public required List<TechConfig> TechResearchConfigs { get; set; }
}