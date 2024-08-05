using LittleConqueror.Infrastructure.Entities.DatabaseEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class UserRepository(DataContext applicationDbContext) 
    : Repository<UserEntity>(applicationDbContext);