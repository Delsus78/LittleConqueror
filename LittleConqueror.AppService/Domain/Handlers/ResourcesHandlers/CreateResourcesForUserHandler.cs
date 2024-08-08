using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

public interface ICreateResourcesForUserHandler
{
    Task<Resources> Handle(CreateResourcesForUserCommand command);
}
public class CreateResourcesForUserHandler(
    IResourcesDatabasePort resourcesDatabase) : ICreateResourcesForUserHandler
{
    public async Task<Resources> Handle(CreateResourcesForUserCommand command)
        => await resourcesDatabase.CreateResources(Resources.CreateDefaultResources(command.UserId));
    
}