using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TechResearchDatabaseAdapter(
    TechResearchRepository techResearchRepository,
    ConfigsRepository configsRepository) : ITechResearchDatabasePort
{
    public async Task<List<TechResearch>> GetAllTechResearchsForUser(long id)
        => await techResearchRepository.GetAllAsync(x => x.UserId == id);

    public async Task<TechResearch?> TryGetInProgressTechResearchForUser(long userId)
        => await techResearchRepository.GetAsync(research => research.UserId == userId && research.ResearchStatus == TechResearchStatus.Researching);

    public async Task<TechResearch> GetOrCreateTechResearchOfUserAsync(long userId, TechResearchType techResearchType, bool disableTracking = true)
    {
        var techResearch = await techResearchRepository.GetAsync(research =>
            research.UserId == userId && research.ResearchType == techResearchType);

        if (techResearch is not null) 
            return techResearch;

        var techConfig = await configsRepository.GetTechConfig(techResearchType) 
                         ?? throw new AppException($"TechConfig with type {techResearchType} not found", 404);
        
        techResearch = new TechResearch
        {
            ResearchCategory = techConfig.Category,
            ResearchType = techResearchType,
            ResearchStatus = TechResearchStatus.Undiscovered,
            UserId = userId
        };
        await techResearchRepository.CreateAsync(techResearch, disableTracking);
        return techResearch;
    }

    public async Task SetStatusForTechResearchForUser(long userId, TechResearchType techResearchType, TechResearchStatus techResearchStatus)
    {
        var techResearch = await GetOrCreateTechResearchOfUserAsync(userId, techResearchType);
        
        techResearch.ResearchStatus = techResearchStatus;

        techResearch.ResearchDate = techResearchStatus switch
        {
            TechResearchStatus.Researching => DateTime.UtcNow,
            TechResearchStatus.Undiscovered => null,
            _ => techResearch.ResearchDate
        };
        await techResearchRepository.UpdateAsync(techResearch);
    }
}