using LittleConqueror.AppService.Domain.Models.Configs;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CompleteTechResearchOfUserIdCommand
{
    public long UserId { get; set; }
    public TechResearchType ResearchType { get; set; }
}