using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Services;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Handlers.TechResearchHandlers;

public interface ISetTechToResearchOfUserIdHandler
{
    Task Handle(SetTechToResearchOfUserIdCommand command);
}
public class SetTechToResearchOfUserIdHandler(
    ITechResearchDatabasePort techResearchDatabase,
    IGetSciencePointsOfUserIdHandler getSciencePointsOfUserIdHandler,
    IUserContext userContext) : ISetTechToResearchOfUserIdHandler
{
    public async Task Handle(SetTechToResearchOfUserIdCommand command)
    {
        if (command.UserId != userContext.UserId)
            throw new AppException("You can't set tech research for another user", 403);
        
        var techResearch = await techResearchDatabase.TryGetInProgressTechResearchForUser(command.UserId);
        if (techResearch is not null)
        {
            if (command.Force)
            {
                await techResearchDatabase.CancelTechResearch(command.UserId, techResearch.ResearchType);
                await SetTechResearchForUser(command.UserId, command.TechResearchType);
            }
            else throw new AppException("You already have a tech research in progress", 400);
        }
    }
    
    private async Task SetTechResearchForUser(long userId, TechResearchType techResearchType)
    {
        var techResearch = await techResearchDatabase.GetTechResearchOfUser(userId, techResearchType);
        if (techResearch.ResearchStatus != TechResearchStatus.Undiscovered)
            throw new AppException("You already have this tech researched or in progress", 400);

        var userSciencesPoints =
            await getSciencePointsOfUserIdHandler.Handle(new GetSciencePointsOfUserIdQuery { UserId = userId });
        
        if (TechResearchesDataDictionaries.Values[techResearchType].cost > userSciencesPoints[techResearch.ResearchCategory])
            throw new AppException("You don't have enough science points", 400);
        
        await techResearchDatabase.SetTechResearchForUser(userId, techResearchType);
    }
}