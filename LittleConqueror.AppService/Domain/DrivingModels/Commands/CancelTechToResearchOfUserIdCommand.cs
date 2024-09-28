using LittleConqueror.AppService.Domain.Models.Configs;

namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CancelTechToResearchOfUserIdCommand
{
    public long UserId { get; set; }
    public TechResearchType TechResearchType { get; set; }
}