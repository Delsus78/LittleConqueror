using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.DrivenPorts.Specifications;

public sealed class TerritoryFromUserIdWithCitiesSpec 
    : BaseSpecification<Territory>
{
    public TerritoryFromUserIdWithCitiesSpec(int userId) 
        : base(t => t.OwnerId == userId)
    {
        AddInclude(t => t.Cities);
    }
}