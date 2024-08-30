using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Handlers.ResourcesHandlers;

namespace LittleConqueror.AppService.Domain.Strategies.ResourceDetailsStrategies.Get;

public interface IGetResourceDetailsStrategy : IStrategy<GetResourceDetailsQuery, Dictionary<ResourceDetailsType, Dictionary<string, int>>>;