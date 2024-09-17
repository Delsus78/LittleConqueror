using System.Collections.Concurrent;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.AppService.Domain.Models.TechResearches;
using LittleConqueror.AppService.Domain.Models.TechResearches.Configs;

namespace LittleConqueror.Infrastructure.Repositories;

public class ConfigsRepository(DataContext applicationDbContext)
    : Repository<Configs>(applicationDbContext)
{
    
    private static readonly ConcurrentStack<TechConfig> techConfigs = new();
    
    public async Task<TechConfig?> GetTechConfig(TechResearchType techResearchType, bool forceRefresh = false)
    {
        if (!techConfigs.IsEmpty && !forceRefresh) 
            return techConfigs.FirstOrDefault(x => x.Type == techResearchType);
        
        var configs = await GetAllAsync(x => true);
        techConfigs.Clear();
        foreach (var techConfig in configs.SelectMany(config => config.TechResearchConfigs))
        {
            techConfigs.Push(techConfig);
        }
        
        return techConfigs.FirstOrDefault(x => x.Type == techResearchType);
    }
    
    public async Task<List<TechConfig>> GetAllTechConfigs(bool forceRefresh = false)
    {
        if (!techConfigs.IsEmpty && !forceRefresh) 
            return techConfigs.ToList();
        
        var configs = await GetAllAsync(x => true);
        techConfigs.Clear();
        foreach (var techConfig in configs.SelectMany(config => config.TechResearchConfigs))
        {
            techConfigs.Push(techConfig);
        }
        return techConfigs.ToList();
    }
    
    public async Task InitAsync(IEnumerable<TechConfig> techConfigsEnumerable)
    {
        var config = (await GetAllAsync(x => true)).FirstOrDefault();
        var techConfigsList = techConfigsEnumerable.ToList();
        
        if (config is null)
        {
            config = new Configs { TechResearchConfigs = techConfigsList };
            await CreateAsync(config);
        }
        else
        {
            config.TechResearchConfigs = techConfigsList;
            await UpdateAsync(config);
        }
        
        techConfigs.Clear();
        foreach (var techConfig in techConfigsList)
        {
            techConfigs.Push(techConfig);
        }
    }
}