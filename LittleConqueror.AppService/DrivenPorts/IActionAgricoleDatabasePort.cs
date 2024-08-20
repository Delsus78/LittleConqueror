using ActionEntities = LittleConqueror.AppService.Domain.Models.Entities.ActionEntities;
namespace LittleConqueror.AppService.DrivenPorts;

public interface IActionAgricoleDatabasePort
{
    Task DeleteActionAgricole(ActionEntities.Agricole actionAgricole);
}