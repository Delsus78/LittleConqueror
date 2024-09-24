using LittleConqueror.Infrastructure.DatabaseAdapters.DbDto;

namespace LittleConqueror.Infrastructure.Repositories;

public class BackgroundJobIdentifierRepository(DataContext applicationDbContext) 
    : Repository<BackgroundJobIdentifierDbDto>(applicationDbContext);