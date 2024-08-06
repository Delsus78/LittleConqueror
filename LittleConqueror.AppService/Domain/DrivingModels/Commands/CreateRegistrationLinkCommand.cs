namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CreateRegistrationLinkCommand
{
    public string Role { get; set; }
    public int FirstCityId { get; set; }
}