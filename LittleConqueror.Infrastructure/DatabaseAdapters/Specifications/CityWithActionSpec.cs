using LittleConqueror.AppService.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LittleConqueror.Infrastructure.DatabaseAdapters.Specifications;

public sealed class CityWithActionSpec 
    : BaseSpecification<City>
{
    public CityWithActionSpec(long cityId) 
        : base(entity => entity.Id == cityId)
    {
        AddInclude(incl => incl.Include(c => c.Action));
    }
}