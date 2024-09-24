using System.Linq.Expressions;
using Hangfire;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Services;

public interface IBackgroundJobService
{
    void StartJobWithDelay<T>(string naturalId, Expression<Func<T, Task>> expr, TimeSpan delay);
    Task StopJob(string jobId);
}
public class BackgroundJobService(IBackgroundJobIdentifiersDatabasePort backgroundJobIdentifiersDatabase) : IBackgroundJobService
{
    public void StartJobWithDelay<T>(string naturalId, Expression<Func<T, Task>> expr, TimeSpan delay)
    {
        var jobId = BackgroundJob.Schedule(expr, delay);
        if (jobId is null)
            throw new AppException("Job failed to start", 500);
        backgroundJobIdentifiersDatabase.SetBackgroundJobIdentifierForNaturalId(naturalId, jobId);
    }

    public async Task StopJob(string naturalId)
    {
        var jobId = await backgroundJobIdentifiersDatabase.GetBackgroundJobIdentifierForNaturalId(naturalId);
        if (jobId is null)
            throw new AppException("Job not found", 404);
        BackgroundJob.Delete(jobId);
        await backgroundJobIdentifiersDatabase.RemoveBackgroundJobIdentifierForNaturalId(naturalId);
    }
}

public enum BackgroundJobNaturalId
{
    // CompleteTechResearchOfUserId_{userId}_{techResearchType} 
    CompleteTechResearchOfUserId
}