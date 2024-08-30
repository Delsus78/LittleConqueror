using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.Domain.Strategies;
using LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

public interface IGetResourceDetailsHandler
{
    Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Handle(GetResourceDetailsQuery query);
}

public class GetResourceDetailsHandler(
    IStrategyContext strategyContext,
    IUserContext userContext) : IGetResourceDetailsHandler
{
    public async Task<Dictionary<ResourceDetailsType, Dictionary<string, int>>> Handle(GetResourceDetailsQuery query)
    {
        if (query.UserId != userContext.UserId)
            throw new AppException("You are not authorized to access this resource", 403);
        
        return await strategyContext.ExecuteStrategy<GetResourceDetailsQuery, Dictionary<ResourceDetailsType, Dictionary<string, int>>, IGetResourceDetailsStrategy>(
            query.ResourceType, query, default);
    }
}

public enum ResourceDetailsType
{
    Production,
    Used
}