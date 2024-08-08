using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class ResourcesDatabaseAdapter(
    ResourcesRepository resourcesRepository) : IResourcesDatabasePort
{
    public async Task<Resources> CreateResources(Resources resources)
    {
        var entityEntry = await resourcesRepository.CreateAsync(resources);
        
        await resourcesRepository.SaveAsync();
        
        return entityEntry.Entity;
    }

    public async Task<Resources?> GetResourcesOfUser(int userId)
        => await resourcesRepository.GetAsync(resources => resources.UserId == userId);
}