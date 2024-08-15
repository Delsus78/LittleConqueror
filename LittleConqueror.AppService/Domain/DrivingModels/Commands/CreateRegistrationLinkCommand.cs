namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CreateRegistrationLinkCommand
{
    public string Role { get; set; }
    public long FirstOsmId { get; set; }
    public char FirstOsmType { get; set; }
}