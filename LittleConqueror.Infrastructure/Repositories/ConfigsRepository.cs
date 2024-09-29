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
        => await GetConfigFromCache(
            techConfigsCache,
            EnsureTechResearchConfigsInitialized,
            x => x.Type == techResearchType,
            forceRefresh
        );

    public async Task<List<TechConfig>> GetAllTechConfigs(bool forceRefresh = false) 
        => await GetAllConfigsFromCache(
            techConfigsCache,
            EnsureTechResearchConfigsInitialized,
            forceRefresh
        );

    public async Task<PopHappinessConfig?> GetPopHappinessConfig(PopHappinessType popHappinessType, bool forceRefresh = false) 
        => await GetConfigFromCache(
            popHappinessConfigsCache,
            EnsurePopHappinessConfigsInitialized,
            x => x.Type == popHappinessType,
            forceRefresh
        );

    public async Task<List<PopHappinessConfig>> GetAllPopHappinessConfigs(bool forceRefresh = false) 
        => await GetAllConfigsFromCache(
            popHappinessConfigsCache, 
            EnsurePopHappinessConfigsInitialized, 
            forceRefresh
        );

    private async Task<T?> GetConfigFromCache<T>(
        ConcurrentStack<T> cache,
        Func<Configs, Task<IEnumerable<T>>> getConfigList,
        Func<T, bool> predicate,
        bool forceRefresh
    ) where T : class
    {
        if (!cache.IsEmpty && !forceRefresh)
            return cache.FirstOrDefault(predicate);

        var config = await GetUniqueConfig();
        await UpdateCache(cache, getConfigList(config));

        return cache.FirstOrDefault(predicate);
    }

    private async Task<List<T>> GetAllConfigsFromCache<T>(
        ConcurrentStack<T> cache,
        Func<Configs, Task<IEnumerable<T>>> getConfigList,
        bool forceRefresh
    )
    {
        if (!cache.IsEmpty && !forceRefresh)
            return cache.ToList();

        var config = await GetUniqueConfig();
        await UpdateCache(cache, getConfigList(config));

        return cache.ToList();
    }

    private async Task UpdateCache<T>(ConcurrentStack<T> cache, Task<IEnumerable<T>> items)
    {
        cache.Clear();
        foreach (var item in await items)
        {
            cache.Push(item);
        }
    }

    private async Task<Configs> GetUniqueConfig()
    {
        var configs = await GetAllAsync(x => true);
        return configs.FirstOrDefault() ?? (await CreateAsync(Configs.CreateDefault())).Entity;
    }
    
    private async Task<IEnumerable<TechConfig>> EnsureTechResearchConfigsInitialized(Configs config)
    {
        if (config.TechResearchConfigs != null) return config.TechResearchConfigs;
        
        config.TechResearchConfigs = Configs.CreateDefault().TechResearchConfigs!;
            
        // Save changes to the database
        await UpdateAsync(config);
        return config.TechResearchConfigs;
    }

    private async Task<IEnumerable<PopHappinessConfig>> EnsurePopHappinessConfigsInitialized(Configs config)
    {
        if (config.PopHappinessConfigs != null) return config.PopHappinessConfigs;
        
        config.PopHappinessConfigs = Configs.CreateDefault().PopHappinessConfigs!;
            
        // Save changes to the database
        await UpdateAsync(config);
        return config.PopHappinessConfigs;
    }
}