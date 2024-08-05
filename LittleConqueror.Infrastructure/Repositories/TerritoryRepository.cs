using LittleConqueror.Infrastructure.Entities.DatabaseEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class TerritoryRepository(DataContext applicationDbContext) 
    : Repository<TerritoryEntity>(applicationDbContext);