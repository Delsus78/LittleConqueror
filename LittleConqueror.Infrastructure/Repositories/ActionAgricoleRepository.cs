using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class ActionAgricoleRepository(DataContext applicationDbContext) 
    : Repository<ActionEntities.Agricole>(applicationDbContext);