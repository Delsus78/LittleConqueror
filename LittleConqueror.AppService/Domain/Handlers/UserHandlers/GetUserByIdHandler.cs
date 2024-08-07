using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.UserHandlers;

public interface IGetUserByIdHandler
{
    public Task<User?> Handle(GetUserByIdQuery query);
}
public class GetUserByIdHandler(IUserDatabasePort userDatabase) : IGetUserByIdHandler
{
    public async Task<User?> Handle(GetUserByIdQuery query)
        => await userDatabase.GetUserById(query.UserId);
}