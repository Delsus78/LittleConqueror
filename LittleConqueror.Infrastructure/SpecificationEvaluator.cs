using LittleConqueror.AppService.Domain.Models.Entities.Base;
using LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;
using Microsoft.EntityFrameworkCore;

namespace LittleConqueror.Infrastructure;

public class SpecificationEvaluator<T> where T : Entity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
    {
        var query = inputQuery;

        // Apply the specification's criteria
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Apply includes and then includes
        if (specification.Includes.Count != 0)
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

        // Apply select
        if (specification.Select != null)
            query = query.Select(specification.Select);

        
        // Apply ordering if specified
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        // Apply paging if enabled
        if (specification.isPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        return query;
    }
}