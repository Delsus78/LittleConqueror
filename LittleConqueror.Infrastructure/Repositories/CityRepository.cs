using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class CityRepository(DataContext applicationDbContext) 
    : Repository<City>(applicationDbContext);