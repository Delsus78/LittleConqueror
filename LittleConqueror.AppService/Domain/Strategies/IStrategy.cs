namespace LittleConqueror.AppService.Domain.Strategies;

public interface IStrategy<in TIn, TOut>
{
    public Task<TOut> Execute(TIn input, CancellationToken cancellationToken);
}