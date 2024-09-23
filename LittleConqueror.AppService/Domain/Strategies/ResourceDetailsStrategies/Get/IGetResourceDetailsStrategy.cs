using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public interface IGetResourceDetailsStrategy : IStrategy<GetResourceDetailsStrategyParams, Dictionary<ResourceDetailsType, Dictionary<string, int>>>;