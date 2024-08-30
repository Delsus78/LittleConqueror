using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

namespace LittleConqueror.API.Models.Dtos;

public class ResourcesDto
{
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Food { get; set; }
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Wood { get; set; }
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Stone { get; set; }
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Iron { get; set; }
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Gold { get; set; }
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Diamond { get; set; }
    public Dictionary<ResourceDetailsType, Dictionary<string, int>> Petrol { get; set; }
}