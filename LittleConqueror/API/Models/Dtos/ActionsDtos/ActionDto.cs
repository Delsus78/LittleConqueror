using System.Text.Json.Serialization;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.API.Models.Dtos.ActionsDtos;

[JsonDerivedType(typeof(ActionAgricoleDto))]
[JsonDerivedType(typeof(ActionMiniereDto))]
[JsonDerivedType(typeof(ActionTechnologiqueDto))]
public abstract class ActionDto
{
    public long Id { get; set; }
    public ActionType ActionType { get; set; }
    public DateTime StartTime { get; set; }
}

public class ActionWithCityDto<T> where T : ActionDto
{
    public T Action { get; set; }
    public LowDataCityDto City { get; set; }
}

public class ActionAgricoleDto : ActionDto
{
    public int? FoodProduction { get; set; }
    public double? AgriculturalFertility { get; set; }
}

public class ActionMiniereDto : ActionDto
{
    public ResourceType ResourceType { get; set; }
}

public class ActionTechnologiqueDto : ActionDto
{
    public TechResearchCategory TechResearchCategory { get; set; }
    public int SciencePoints { get; set; }
    public double? TechnologiqueEfficiency { get; set; }
}