using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class ActionRepository(DataContext applicationDbContext) 
    : Repository<ActionEntities.Action>(applicationDbContext);