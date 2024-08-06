
using LittleConqueror.Infrastructure.Entities.DatabaseEntities;

namespace LittleConqueror.Infrastructure.Repositories;

public class AuthUserRepository(DataContext applicationDbContext) : Repository<AuthUserEntity>(applicationDbContext);