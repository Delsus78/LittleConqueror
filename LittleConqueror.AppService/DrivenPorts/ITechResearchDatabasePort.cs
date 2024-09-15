using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITechResearchDatabasePort
{
    Task<List<TechResearch>> GetAllTechResearchsForUser(long id);
    Task<TechResearch?> TryGetInProgressTechResearchForUser(long userId);
    Task<TechResearch> GetTechResearchOfUser(long userId, TechResearchType techResearchType);
    Task CancelTechResearch(long userId, TechResearchType techResearchType);
    Task SetTechResearchForUser(long userId, TechResearchType techResearchType);
}