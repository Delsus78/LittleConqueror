using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface IResourcesDatabasePort
{
    public Task<Resources> CreateResources(Resources resources);
    public Task<Resources?> GetResourcesOfUser(int userId);
}