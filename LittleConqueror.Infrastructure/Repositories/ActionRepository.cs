using LittleConqueror.AppService.Domain.Logic;
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
}