using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.AuthHandlers;

public interface IGetAuthenticatedUserByIdHandler
{
    Task<AuthUser> Handle(GetAuthenticatedUserByIdQuery query);
}
public class GetAuthenticatedUserByIdHandler(
    IAuthUserDatabasePort authUserDatabase) : IGetAuthenticatedUserByIdHandler
{
    public async Task<AuthUser> Handle(GetAuthenticatedUserByIdQuery query)
    {
        var entity = await authUserDatabase.GetAuthUserById(query.UserId);
        if (entity == null)
            throw new AppException("User not found", 404);
        
        return entity;
    }
}