namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class RegisterAuthUserCommand
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ValidRegistrationLink { get; set; }
}