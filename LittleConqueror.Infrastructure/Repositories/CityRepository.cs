using LittleConqueror.AppService.DomainEntities;
using LittleConqueror.Persistence;
using LittleConqueror.Persistence.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class CityRepository(DataContext applicationDbContext) : Repository<CityEntity>(applicationDbContext);