using LittleConqueror.AppService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;

public sealed class TerritoryFromUserIdWithCitiesSpec 
    : BaseSpecification<Territory>
{
    public TerritoryFromUserIdWithCitiesSpec(long userId) 
        : base(t => t.OwnerId == userId)
    {
        AddInclude(incl => incl.Include(t => t.Cities));
    }
}

public sealed class TerritoryFromUserIdWithCitiesAndActionSpec 
    : BaseSpecification<Territory>
{
    public TerritoryFromUserIdWithCitiesAndActionSpec(long userId) 
        : base(t => t.OwnerId == userId)
    {
        AddInclude(incl => incl.Include(t => t.Cities));
        AddInclude(incl => 
            incl.Include(t => t.Cities)
                .ThenInclude(c => c.Action));
    }
}