using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

public interface IGetResourcesForUserHandler
{
    Task<Resources> Handle(GetResourcesForUserQuery query);
}

public class GetResourcesForUserHandler(
    IResourcesDatabasePort resourcesDatabase) : IGetResourcesForUserHandler
{
    public async Task<Resources> Handle(GetResourcesForUserQuery query)
        => await resourcesDatabase.GetResourcesOfUser(query.UserId) ??
              throw new AppException("Resources not found", 404);
}