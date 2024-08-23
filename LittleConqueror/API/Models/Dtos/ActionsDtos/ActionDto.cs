using System.Text.Json.Serialization;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.API.Models.Dtos.ActionsDtos;

[JsonDerivedType(typeof(ActionAgricoleDto))]
[JsonDerivedType(typeof(ActionMiniereDto))]
public abstract class ActionDto
{
    public long Id { get; set; }
    public ActionType ActionType { get; set; }
    public DateTime StartTime { get; set; }
}

public class ActionAgricoleDto : ActionDto;

public class ActionMiniereDto : ActionDto
{
    public ResourceType ResourceType { get; set; }
}