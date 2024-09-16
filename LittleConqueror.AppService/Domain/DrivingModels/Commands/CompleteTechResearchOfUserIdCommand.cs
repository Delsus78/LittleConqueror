using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CompleteTechResearchOfUserIdCommand
{
    public long UserId { get; set; }
    public TechResearchType ResearchType { get; set; }
}