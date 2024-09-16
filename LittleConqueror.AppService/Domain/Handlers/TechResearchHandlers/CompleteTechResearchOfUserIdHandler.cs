using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface ICompleteTechResearchOfUserIdHandler
{
    Task Handle(CompleteTechResearchOfUserIdCommand command);
}
public class CompleteTechResearchOfUserIdHandler(ITechResearchDatabasePort techResearchDatabase) : ICompleteTechResearchOfUserIdHandler
{
    public async Task Handle(CompleteTechResearchOfUserIdCommand command) 
        => await techResearchDatabase.SetStatusForTechResearchForUser(command.UserId, command.ResearchType, TechResearchStatus.Researched);
}