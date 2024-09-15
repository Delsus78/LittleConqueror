using LittleConqueror.AppService.Domain.Models.TechResearches;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class SetTechToResearchOfUserIdCommand
{
    public long UserId { get; set; }
    public TechResearchType TechResearchType { get; set; }
    public bool Force { get; set; }
}