using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;
using LittleConqueror.AppService.Domain.Models;
using LittleConqueror.AppService.Domain.Models.Entities;
using LittleConqueror.Infrastructure.DatabaseAdapters.DbDto;
using LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;
using Microsoft.EntityFrameworkCore;
using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class ActionRepository(DataContext applicationDbContext)
    : Repository<ActionEntities.Action>(applicationDbContext)
{
    public async Task<ActionPaginableListDbDto> GetPaginatedActionsByUserId(long userId, int skip, int take)
    {
        var query = _dbSet
            .Where(a => a.City.Territory.OwnerId == userId)
            .OrderByDescending(x => x.StartTime)
            .Select(x => CityWithActionSpecExtensions.PopulateWithCity(x, new City
            {
                OsmType = x.City.OsmType,
                Id = x.City.Id,
                AddressType = x.City.AddressType,
                Longitude = x.City.Longitude,
                Latitude = x.City.Latitude,
                Population = x.City.Population,
                Name = x.City.Name
            }));

        var totalActions = await query.CountAsync();

        var actions = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return new ActionPaginableListDbDto(totalActions, actions);
    }
    
    public async Task<int> ComputeTotalFood(long userId)
    {
        // Étape 1 : Récupérer uniquement les données nécessaires (filtrage côté serveur)
        var cityAgricole = await _dbSet
            .OfType<ActionEntities.Agricole>()
            .Where(a => a.City.Territory.OwnerId == userId)
            .Select(a => new City
            {
                Id = a.City.Id,
                OsmType = a.City.OsmType,
                Latitude = a.City.Latitude,
                Longitude = a.City.Longitude,
                Population = a.City.Population
            }).ToListAsync();

        // Étape 2 : Calculer le total de la nourriture côté client
        var totalFood = cityAgricole
            .Sum(AgricoleExpressions.GetFoodProductionExpression);
        return totalFood;
    }
    
    public async Task<int> ComputeUsedFood(long userId, ActionType? actionType = null)
    {
        var result = 0;
        
        if (actionType is null or ActionType.Miniere)
            result += await _dbSet
                .OfType<ActionEntities.Miniere>()
                .Where(a => a.City.Territory.OwnerId == userId)
                .SumAsync(MiniereExpressions.GetFoodPriceExpression());

        if (actionType is null or ActionType.Militaire)
            result += await _dbSet
                .OfType<ActionEntities.Militaire>()
                .Where(a => a.City.Territory.OwnerId == userId)
                .SumAsync(MilitaireExpressions.GetFoodPriceExpression());

        if (actionType is null or ActionType.Diplomatique)
            result += await _dbSet
                .OfType<ActionEntities.Diplomatique>()
                .Where(a => a.City.Territory.OwnerId == userId)
                .SumAsync(DiplomatiqueExpressions.GetFoodPriceExpression());

        if (actionType is null or ActionType.Espionnage)
            result += await _dbSet
                .OfType<ActionEntities.Espionnage>()
                .Where(a => a.City.Territory.OwnerId == userId)
                .SumAsync(EspionnageExpressions.GetFoodPriceExpression());

        if (actionType is null or ActionType.Technologique)
            result += await _dbSet
                .OfType<ActionEntities.Technologique>()
                .Where(a => a.City.Territory.OwnerId == userId)
                .SumAsync(TechnologiqueExpressions.GetFoodPriceExpression());

        return result;
    }
    
    public async Task<int> ComputeTotalWood(long userId)
    {
        // var totalWood = await _dbSet
        //     .OfType<ActionEntities.Miniere>()
        //     .Where(a => a.City.Territory.OwnerId == userId)
        //     .SumAsync(MiniereExpressions.GetWoodProductionExpression());
        //
        // return totalWood;
        return 1;
    }
    
    public async Task<int> ComputeUsedWood(long userId, ActionType? actionType = null)
    {
        return 1;
    }
    
    public async Task<int> ComputeTotalStone(long userId)
    {
        // var totalStone = await _dbSet
        //     .OfType<ActionEntities.Miniere>()
        //     .Where(a => a.City.Territory.OwnerId == userId)
        //     .SumAsync(MiniereExpressions.GetStoneProductionExpression());
        //
        // return totalStone;
        return 1;
    }
    
    public async Task<int> ComputeUsedStone(long userId, ActionType? actionType = null)
    {
        return 1;
    }
    
    public async Task<int> ComputeTotalIron(long userId)
    {
        // var totalIron = await _dbSet
        //     .OfType<ActionEntities.Miniere>()
        //     .Where(a => a.City.Territory.OwnerId == userId)
        //     .SumAsync(MiniereExpressions.GetIronProductionExpression());
        //
        // return totalIron;
        return 1;
    }
    
    public async Task<int> ComputeUsedIron(long userId, ActionType? actionType = null)
    {
        return 1;
    }
    
    public async Task<int> ComputeTotalGold(long userId)
    {
        // var totalGold = await _dbSet
        //     .OfType<ActionEntities.Miniere>()
        //     .Where(a => a.City.Territory.OwnerId == userId)
        //     .SumAsync(MiniereExpressions.GetGoldProductionExpression());
        //
        // return totalGold;
        return 1;
    }
    
    public async Task<int> ComputeUsedGold(long userId, ActionType? actionType = null)
    {
        return 1;
    }
    
    public async Task<int> ComputeTotalDiamond(long userId)
    {
        // var totalDiamond = await _dbSet
        //     .OfType<ActionEntities.Miniere>()
        //     .Where(a => a.City.Territory.OwnerId == userId)
        //     .SumAsync(MiniereExpressions.GetDiamondProductionExpression());
        //
        // return totalDiamond;
        return 1;
    }
    
    public async Task<int> ComputeUsedDiamond(long userId, ActionType? actionType = null)
    {
        return 1;
    }
    
    public async Task<int> ComputeTotalPetrol(long userId)
    {
        // var totalPetrol = await _dbSet
        //     .OfType<ActionEntities.Miniere>()
        //     .Where(a => a.City.Territory.OwnerId == userId)
        //     .SumAsync(MiniereExpressions.GetPetrolProductionExpression());
        //
        // return totalPetrol;
        return 1;
    }
    
    public async Task<int> ComputeUsedPetrol(long userId, ActionType? actionType = null)
    {
        return 1;
    }
    
    public async Task<int> ComputeTotalResearchPoints(long userId)
    {
        var totalResearchPoints = await _dbSet
            .OfType<ActionEntities.Technologique>()
            .Where(a => a.City.Territory.OwnerId == userId)
            .SumAsync(TechnologiqueExpressions.GetResearchPointsProductionExpression());
        
        return totalResearchPoints;
    }
    
    public async Task<int> ComputeUsedResearchPoints(long userId, ActionType? actionType = null)
    {
        return 1;
    }
}