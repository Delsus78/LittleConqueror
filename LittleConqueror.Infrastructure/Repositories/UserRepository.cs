using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.Infrastructure.Repositories;

public class UserRepository(DataContext applicationDbContext) 
    : Repository<User>(applicationDbContext);