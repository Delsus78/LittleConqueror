using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.AppService.Exceptions;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TechResearchDatabaseAdapter(TechResearchRepository techResearchRepository) : ITechResearchDatabasePort
{
    public async Task<List<TechResearch>> GetAllTechResearchsForUser(long id)
        => await techResearchRepository.GetAllAsync(x => x.UserId == id);

    public async Task<TechResearch?> TryGetInProgressTechResearchForUser(long userId)
        => await techResearchRepository.GetAsync(research => research.UserId == userId && research.ResearchStatus == TechResearchStatus.Researching);

    public async Task<TechResearch> GetTechResearchOfUser(long userId, TechResearchType techResearchType)
        => await techResearchRepository.GetAsync(research =>
            research.UserId == userId && research.ResearchType == techResearchType) ?? new TechResearch
        {
            ResearchCategory = TechResearchesDataDictionaries.Values[techResearchType].category,
            ResearchType = techResearchType,
            ResearchStatus = TechResearchStatus.Undiscovered,
            UserId = userId
        };

    public async Task CancelTechResearch(long userId, TechResearchType techResearchType)
    {
        var techResearch = await techResearchRepository.GetAsync(research =>
            research.UserId == userId && research.ResearchType == techResearchType && research.ResearchStatus == TechResearchStatus.Researching)
                           ?? throw new AppException("Tech research is not in progress", 400);
        
        techResearch.ResearchStatus = TechResearchStatus.Undiscovered;
        
        await techResearchRepository.UpdateAsync(techResearch);
    }

    public async Task SetTechResearchForUser(long userId, TechResearchType techResearchType)
    {
        var techResearch = await techResearchRepository.GetAsync(research =>
                               research.UserId == userId && research.ResearchType == techResearchType && research.ResearchStatus == TechResearchStatus.Researching)
                           ?? throw new AppException("Tech research is not in progress", 400);
        
        techResearch.ResearchStatus = TechResearchStatus.Researching;
        
        await techResearchRepository.UpdateAsync(techResearch);
    }
}