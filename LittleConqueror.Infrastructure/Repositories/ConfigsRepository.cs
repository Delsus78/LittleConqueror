using System.Collections.Concurrent;
using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class ConfigsRepository(DataContext applicationDbContext)
    : Repository<Configs>(applicationDbContext)
{
    
    private static readonly ConcurrentStack<TechConfig> techConfigsCache = new();
    private static readonly ConcurrentStack<PopHappinessConfig> popHappinessConfigsCache = new();
    
    public async Task<TechConfig?> GetTechConfig(TechResearchType techResearchType, bool forceRefresh = false)
    {
        if (!techConfigsCache.IsEmpty && !forceRefresh)
            return techConfigsCache.FirstOrDefault(x => x.Type == techResearchType);
        
        var configs = await GetAllAsync(x => true);
        techConfigsCache.Clear();
        
        var techResearchConfigs = configs
            .FindAll(config => config.TechResearchConfigs != null)
            .SelectMany(config => config.TechResearchConfigs!)
            .ToList();
        
        foreach (var techConfig in techResearchConfigs)
        {
            techConfigsCache.Push(techConfig);
        }
        
        return techConfigsCache.FirstOrDefault(x => x.Type == techResearchType);
    }
    
    public async Task<List<TechConfig>> GetAllTechConfigs(bool forceRefresh = false)
    {
        if (!techConfigsCache.IsEmpty && !forceRefresh) 
            return techConfigsCache.ToList();
        
        var configs = await GetAllAsync(x => true);
        techConfigsCache.Clear();
        
        var techResearchConfigs = configs
            .FindAll(config => config.TechResearchConfigs != null)
            .SelectMany(config => config.TechResearchConfigs!)
            .ToList();
        
        foreach (var techConfig in techResearchConfigs)
        {
            techConfigsCache.Push(techConfig);
        }
        return techConfigsCache.ToList();
    }
    
    public async Task<PopHappinessConfig?> GetPopHappinessConfig(PopHappinessType popHappinessType, bool forceRefresh = false)
    {
        if (!popHappinessConfigsCache.IsEmpty && !forceRefresh)
            return popHappinessConfigsCache.FirstOrDefault(x => x.Type == popHappinessType);
        
        var configs = await GetAllAsync(x => true);
        popHappinessConfigsCache.Clear();
        
        var popHappinessConfigs = configs
            .FindAll(config => config.PopHappinessConfigs != null)
            .SelectMany(config => config.PopHappinessConfigs!)
            .ToList();
        
        foreach (var popHappinessConfig in popHappinessConfigs)
        {
            popHappinessConfigsCache.Push(popHappinessConfig);
        }
        return popHappinessConfigsCache.FirstOrDefault(x => x.Type == popHappinessType);
    }
    
    public async Task<List<PopHappinessConfig>> GetAllPopHappinessConfigs(bool forceRefresh = false)
    {
        if (!popHappinessConfigsCache.IsEmpty && !forceRefresh)
            return popHappinessConfigsCache.ToList();
        
        var configs = await GetAllAsync(x => true);
        popHappinessConfigsCache.Clear();
        
        var popHappinessConfigs = configs
            .FindAll(config => config.PopHappinessConfigs != null)
            .SelectMany(config => config.PopHappinessConfigs!)
            .ToList();
        
        foreach (var popHappinessConfig in popHappinessConfigs)
        {
            popHappinessConfigsCache.Push(popHappinessConfig);
        }
        
        return popHappinessConfigsCache.ToList();
    }
}