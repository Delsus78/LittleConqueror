using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using Newtonsoft.Json;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands.ActionsCommands;


public class SetActionToCityCommand
{
    public long CityId { get; set; }
    public ActionType ActionType { get; set; }
    
    private ResourceType _resourceType;
    public ResourceType ResourceType
    {
        get => _resourceType;
        set 
        {
            if (ActionType != ActionType.Miniere)
                throw new ArgumentException("ResourceType can only be set for Miniere action type");
            _resourceType = value;
        }
    }

    [JsonIgnore]
    internal City? City { get; set; }
}