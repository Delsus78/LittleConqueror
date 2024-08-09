namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class CreateUserCommand
{
    public string Name { get; set; }
    public long FirstOsmId { get; set; }
    public char FirstOsmType { get; set; }
}