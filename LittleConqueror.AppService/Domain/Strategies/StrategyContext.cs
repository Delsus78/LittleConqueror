using LittleConqueror.AppService.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace LittleConqueror.AppService.Domain.Strategies;

public interface IStrategyContext
{
    Task<TOut> ExecuteStrategy<TIn, TOut, TStrategy>(object key, TIn model, CancellationToken cancellationToken)
        where TStrategy : IStrategy<TIn, TOut>;
}
public class StrategyContext(IServiceProvider serviceProvider) : IStrategyContext
{
    public async Task<TOut> ExecuteStrategy<TIn, TOut, TStrategy>(object key, TIn model, CancellationToken cancellationToken)
        where TStrategy : IStrategy<TIn, TOut>
    {
        ArgumentNullException.ThrowIfNull(key);
        ArgumentNullException.ThrowIfNull(model);

        var strategy = serviceProvider.GetKeyedService<TStrategy>(key);
        if (strategy == null)
            throw new AppException($"Strategy with key {key} not found", 404);

        return await strategy.Execute(model, cancellationToken);
    }
}