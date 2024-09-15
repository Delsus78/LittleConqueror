using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface ICancelTechResearchOfUserIdHandler
{
    Task Handle(CancelTechToResearchOfUserIdCommand command);
}
public class CancelTechResearchOfUserIdHandler(
    ITechResearchDatabasePort techResearchDatabase,
    IUserContext userContext) : ICancelTechResearchOfUserIdHandler
{
    public Task Handle(CancelTechToResearchOfUserIdCommand command)
    {
        if (command.UserId != userContext.UserId)
            throw new UnauthorizedAccessException("User can only cancel their own tech researches");
        
        return techResearchDatabase.CancelTechResearch(command.UserId, command.TechResearchType);
    }
}