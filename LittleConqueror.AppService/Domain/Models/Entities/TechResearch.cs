using System.ComponentModel.DataAnnotations.Schema;
using LittleConqueror.AppService.Domain.Models.Entities.Base;
using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class TechResearch : Entity
{
    public DateTime ResearchDate { get; set; }
    public TechResearchCategories ResearchCategory { get; set; }
    public TechResearchTypes ResearchType { get; set; }
    public TechResearchStatus ResearchStatus { get; set; }

    // 1:1 relationship
    public long UserId { get; set; }
    public User User { get; set; }
}