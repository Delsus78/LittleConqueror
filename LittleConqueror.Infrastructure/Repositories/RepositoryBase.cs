using System.Linq.Expressions;
using LittleConqueror.AppService.Domain.Models.Entities.Base;
using LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LittleConqueror.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool disableTracking = true);
    Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec);
    Task<int> CountAsync(ISpecification<T> spec);
    Task<EntityEntry<T>> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
    Task SaveAsync();
}
public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly DataContext _applicationDbContext;
    private readonly DbSet<T> _dbSet;

    public Repository(DataContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        _dbSet = _applicationDbContext.Set<T>();
    }


    public Task<int> CountAsync(ISpecification<T> spec)
    {
        throw new NotImplementedException();
    }

    public async Task<EntityEntry<T>> CreateAsync(T entity)
    {
        var response = await _dbSet.AddAsync(entity);
        await SaveAsync();
        
        return response;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Entry(entity).State = EntityState.Modified;
        await SaveAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>>? filter = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }
        return await query.FirstOrDefaultAsync(); // query will be executed here, deffered execution
    }

    public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbSet;

        if (disableTracking)
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
    
    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), spec);
    }
}