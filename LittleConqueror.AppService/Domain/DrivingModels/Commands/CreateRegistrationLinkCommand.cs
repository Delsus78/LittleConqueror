namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CreateRegistrationLinkCommand
{
    public string Role { get; set; }
    public int FirstOsmId { get; set; }
    public char FirstOsmType { get; set; }
}