using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class ResourcesRepository(DataContext applicationDbContext) 
    : Repository<Resources>(applicationDbContext);