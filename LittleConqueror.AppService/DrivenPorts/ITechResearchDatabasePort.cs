using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts;

public interface ITechResearchDatabasePort
{
    Task<List<TechResearch>> GetAllTechResearchsForUser(long id);
}