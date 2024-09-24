using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface ICancelTechResearchOfUserIdHandler
{
    Task Handle(CancelTechToResearchOfUserIdCommand command);
}
public class CancelTechResearchOfUserIdHandler(
    ITechResearchDatabasePort techResearchDatabase,
    IBackgroundJobService backgroundJobService,
    IUserContext userContext) : ICancelTechResearchOfUserIdHandler
{
    public async Task Handle(CancelTechToResearchOfUserIdCommand command)
    {
        if (userContext.IsUnauthorized(command.UserId))
            throw new UnauthorizedAccessException("User can only cancel their own tech researches");
        
        // cancel job task
        await backgroundJobService.StopJob($"{BackgroundJobNaturalId.CompleteTechResearchOfUserId}_{command.UserId}_{command.TechResearchType}");
        
        await techResearchDatabase.SetStatusForTechResearchForUser(command.UserId, command.TechResearchType, TechResearchStatus.Undiscovered);
    }
}