namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class AuthenticateUserCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
}