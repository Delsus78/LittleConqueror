using LittleConqueror.Infrastructure.Entities.DatabaseEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class CityRepository(DataContext applicationDbContext) 
    : Repository<CityEntity>(applicationDbContext);