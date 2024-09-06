using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class TechResearchRepository(DataContext applicationDbContext) 
    : Repository<TechResearch>(applicationDbContext);