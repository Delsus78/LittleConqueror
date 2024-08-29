using LittleConqueror.AppService.Domain.Logic;
using LittleConqueror.AppService.Domain.Logic.ActionsHelpers;
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
        var baseFertility = GeoProceduralConfigs.BaseFertility;

        var totalFood = await _dbSet
            .OfType<ActionEntities.Agricole>()
            .Where(a => a.City.Territory.OwnerId == userId)
            .SumAsync(AgricoleExpressions.GetFoodProductionExpression(baseFertility));
        
        return totalFood;
    }
    
    public async Task<int> ComputeAvailableFood(long userId)
    {
        var baseFertility = GeoProceduralConfigs.BaseFertility;

        // Calculer la production de nourriture pour les actions Agricole
        var totalFoodProduction = await _dbSet
            .OfType<ActionEntities.Agricole>()
            .Where(a => a.City.Territory.OwnerId == userId)
            .SumAsync(AgricoleExpressions.GetFoodProductionExpression(baseFertility));

        // Calculer les coûts de nourriture pour toutes les autres actions
        var totalFoodCosts = await _dbSet
            .Where(a => a is ActionEntities.Miniere 
                        || a is ActionEntities.Militaire 
                        || a is ActionEntities.Diplomatique 
                        || a is ActionEntities.Espionnage 
                        || a is ActionEntities.Technologique)
            .Where(a => a.City.Territory.OwnerId == userId)
            .SumAsync(a => 
                a is ActionEntities.Miniere ? MiniereExpressions.GetFoodPriceExpression().Compile().Invoke((ActionEntities.Miniere)a) 
                : a is ActionEntities.Militaire ? MilitaireExpressions.GetFoodPriceExpression().Compile().Invoke((ActionEntities.Militaire)a)
                : a is ActionEntities.Diplomatique ? DiplomatiqueExpressions.GetFoodPriceExpression().Compile().Invoke((ActionEntities.Diplomatique)a)
                : a is ActionEntities.Espionnage ? EspionnageExpressions.GetFoodPriceExpression().Compile().Invoke((ActionEntities.Espionnage)a)
                : a is ActionEntities.Technologique ? TechnologiqueExpressions.GetFoodPriceExpression().Compile().Invoke((ActionEntities.Technologique)a)
                : 0
            );

        // Retourner la différence entre la production et les coûts
        return totalFoodProduction - totalFoodCosts;
    }
}