using System.Text.Json.Serialization;
using LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;

namespace LittleConqueror.API.Models.Dtos.ActionsDtos;

[JsonDerivedType(typeof(ActionAgricoleDto))]
public abstract class ActionDto
{
    public long Id { get; set; }
    public ActionType ActionType { get; set; }
    public DateTime StartTime { get; set; }
}