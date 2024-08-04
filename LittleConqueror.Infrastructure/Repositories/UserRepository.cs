using LittleConqueror.Persistence.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class UserRepository(DataContext applicationDbContext) : Repository<UserEntity>(applicationDbContext);