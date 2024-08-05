using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LittleConqueror.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true);
    Task<EntityEntry<T>> CreateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task SaveAsync();
}
public class Repository<T> : IRepository<T> where T : class
{
    private readonly DataContext _applicationDbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(DataContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<T>();
    }


    public async Task<EntityEntry<T>> CreateAsync(T entity)
    {
        var response = await _dbSet.AddAsync(entity);
        await SaveAsync();
        
        return response;
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync(); // query will be executed here, deffered execution
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.ToListAsync(); // query will be executed here, deffered execution

    }

    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.AnyAsync(filter);
    }
    
    public async Task SaveAsync()
    {
        await _applicationDbContext.SaveChangesAsync();
    }
}