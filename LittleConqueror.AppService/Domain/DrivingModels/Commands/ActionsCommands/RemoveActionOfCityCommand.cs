using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using Newtonsoft.Json;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;

public class RemoveActionOfCityCommand
{
    public ActionType ActualActionType { get; set; }
    public long CityId { get; set; }

    [JsonIgnore]
    internal City? City { get; set; }
}