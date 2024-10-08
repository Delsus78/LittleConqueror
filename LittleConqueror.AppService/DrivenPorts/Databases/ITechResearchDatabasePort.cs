using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITechResearchDatabasePort
{
    Task<List<TechResearch>> GetAllTechResearchsForUser(long id);
    Task<TechResearch?> TryGetInProgressTechResearchForUser(long userId);
    Task<TechResearch> GetOrCreateTechResearchOfUserAsync(long userId, TechResearchType techResearchType, bool disableTracking = true);
    Task SetStatusForTechResearchForUser(long userId, TechResearchType techResearchType, TechResearchStatus techResearchStatus);
}