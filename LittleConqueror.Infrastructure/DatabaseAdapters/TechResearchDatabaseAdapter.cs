using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.DrivenPorts;
using LittleConqueror.Infrastructure.Repositories;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TechResearchDatabaseAdapter(TechResearchRepository techResearchRepository) : ITechResearchDatabasePort
{
    public async Task<List<TechResearch>> GetAllTechResearchsForUser(long id)
        => await techResearchRepository.GetAllAsync(x => x.UserId == id);
}