using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.Domain.DrivingModels.Queries;
using LittleConqueror.AppService.Domain.Models.Configs;
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
    ICancelTechResearchOfUserIdHandler cancelTechResearchOfUserIdHandler,
    IBackgroundJobService backgroundJobService,
    ITechResearchConfigsDatabasePort techResearchConfigsDatabase,
    IGetPopulatedTechResearchesFromUserResearchListHandler getPopulatedTechResearchesFromUserResearchListHandler,
    IUserContext userContext) : ISetTechToResearchOfUserIdHandler
{
    public async Task Handle(SetTechToResearchOfUserIdCommand command)
    {
        if (userContext.IsUnauthorized(command.UserId))
            throw new AppException("You can't set tech research for another user", 403);

        var techResearchType = command.TechResearchType;
        var userId = command.UserId;
        var techResearch = await techResearchDatabase.GetOrCreateTechResearchOfUserAsync(userId, techResearchType);
        var techConfig = await techResearchConfigsDatabase.GetTechConfigByType(techResearchType);
        
        var techResearchPopulated = (await getPopulatedTechResearchesFromUserResearchListHandler.Handle(new GetPopulatedTechResearchesFromUserResearchListQuery
        {
            UserId = userId,
            TechResearches = new []{ techResearch }
        })).FirstOrDefault() ?? throw new AppException("Tech research not found", 404);
        var techResearchAvailabilities = techResearchPopulated.Availabilities;
        
        // is valid
        if (techResearchAvailabilities.Count == 0)
        {
            await SetTechResearchForUser(userId, techResearchType, techConfig);
            return;
        }
        
        // is not valid
        
        // special case: force research if another tech is already in progress
        if (command.Force 
            && techResearchAvailabilities.Count == 1 
            && techResearchAvailabilities.Contains(TechResearchAvailabilityEnum.AnotherTechResearchIsAlreadyInProgress))
        {
            await cancelTechResearchOfUserIdHandler.Handle(new CancelTechToResearchOfUserIdCommand
            {
                UserId = command.UserId,
                TechResearchType = techResearch.ResearchType
            });
            
            // after canceling, set tech research
            await SetTechResearchForUser(command.UserId, techResearchType, techConfig);
        }
        else throw new AppException("Tech research is not valid : " 
                                    + string.Join(", ", techResearchAvailabilities), 400);
    }
    
    private async Task SetTechResearchForUser(long userId, TechResearchType techResearchType, TechConfig techConfig)
    {
        
        await techResearchDatabase.SetStatusForTechResearchForUser(userId, techResearchType, TechResearchStatus.Researching);
        
        // start hangfire job
        backgroundJobService.StartJobWithDelay<ICompleteTechResearchOfUserIdHandler>(
             $"{BackgroundJobNaturalId.CompleteTechResearchOfUserId}_{userId}_{techResearchType}",
            handler => handler.Handle(new CompleteTechResearchOfUserIdCommand
            {
                UserId = userId,
                ResearchType = techResearchType
            }), 
            techConfig.ResearchTime);
    }
}