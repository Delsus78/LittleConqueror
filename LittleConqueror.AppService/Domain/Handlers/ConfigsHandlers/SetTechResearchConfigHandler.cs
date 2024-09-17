using LittleConqueror.AppService.Domain.DrivingModels.Commands;
using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.AppService.Domain.Handlers.ConfigsHandlers;

public interface ISetTechResearchConfigHandler
{
    Task Handle(SetTechResearchConfigCommand command);
}
public class SetTechResearchConfigHandler(ITechResearchConfigsProviderPort techResearchConfigsProvider) : ISetTechResearchConfigHandler
{
    public async Task Handle(SetTechResearchConfigCommand command)
        => await techResearchConfigsProvider.InitTechConfigs(command.TechConfigs);
}