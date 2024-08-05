using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers;

public interface IGetUserByIdHandler
{
    public Task<User?> Handle(GetUserByIdQuery query);
}
public class GetUserByIdHandler(IUserDatabasePort userDatabase) : IGetUserByIdHandler
{
    public async Task<User?> Handle(GetUserByIdQuery query)
        => await userDatabase.GetUserById(query.UserId);
}