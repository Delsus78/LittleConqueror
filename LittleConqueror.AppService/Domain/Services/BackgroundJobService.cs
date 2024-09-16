using System.Linq.Expressions;
using Hangfire;

namespace LittleConqueror.AppService.Domain.Services;

public interface IBackgroundJobService
{
    void StartJobWithDelay<T>(Expression<Func<T, Task>> expr, TimeSpan delay);
}
public class BackgroundJobService : IBackgroundJobService
{
    public void StartJobWithDelay<T>(Expression<Func<T, Task>> expr, TimeSpan delay)
    {
        BackgroundJob.Schedule(expr, delay);
    }
}