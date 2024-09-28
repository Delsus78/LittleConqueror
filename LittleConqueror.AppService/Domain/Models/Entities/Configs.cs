using LittleConqueror.AppService.Domain.Models.Configs;
using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class Configs : Entity
{
    public List<TechConfig>? TechResearchConfigs { get; set; }
    public List<PopHappinessConfig>? PopHappinessConfigs { get; set; }
    
    public static Configs CreateDefault()
    {
        return new Configs
        {
            TechResearchConfigs = TechResearchesDataDictionariesInitialisator.Values
                .Select(x => new TechConfig
                {
                    Description = x.Value.description,
                    Name = x.Value.name,
                    Type = x.Key,
                    Category = x.Value.category,
                    Cost = x.Value.cost,
                    ResearchTime = x.Value.researchTime,
                    PreReqs = x.Value.preReqs
                }).ToList(),
            PopHappinessConfigs = PopHappinessinititialisator.Values
                .Select(x => new PopHappinessConfig
                {
                    Type = x.Key,
                    Coef = x.Value
                }).ToList()
        };
    }
}