using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CancelTechToResearchOfUserIdCommand
{
    public long UserId { get; set; }
    public TechResearchType TechResearchType { get; set; }
}