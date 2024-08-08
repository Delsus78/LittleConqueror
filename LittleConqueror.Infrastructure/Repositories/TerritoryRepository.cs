using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class TerritoryRepository(DataContext applicationDbContext) 
    : Repository<Territory>(applicationDbContext);