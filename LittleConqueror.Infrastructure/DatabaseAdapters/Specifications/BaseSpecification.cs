using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;

public interface ISpecification<T>
{
    Expression<Func<T, bool>>? Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, T>>? Select { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }

    int Take { get; }
    int Skip { get; }
    bool isPagingEnabled { get; }
}
public abstract class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; } = criteria;
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, T>>? Select { get; private set; }
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool isPagingEnabled { get; private set; }

    protected virtual void AddInclude(Expression<Func<T,Object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    protected virtual void ApplySelect(Expression<Func<T, T>> selectExpression)
    {
        Select = selectExpression;
    }
    
    protected virtual void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        isPagingEnabled = true;
    }
    
    protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }
}