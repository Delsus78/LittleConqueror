using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class AuthUserRepository(DataContext applicationDbContext) 
    : Repository<AuthUser>(applicationDbContext);