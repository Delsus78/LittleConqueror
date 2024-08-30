using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetResourceDetailsQuery
{
    public long UserId { get; set; }
    public ResourceType ResourceType { get; set; }
}