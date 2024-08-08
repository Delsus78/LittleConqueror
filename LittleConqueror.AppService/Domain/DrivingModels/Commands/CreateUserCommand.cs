namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CreateUserCommand
{
    public string Name { get; set; }
    public int FirstOsmId { get; set; }
    public char FirstOsmType { get; set; }
}