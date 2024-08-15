namespace LittleConqueror.AppService.Domain.DrivingModels.Commands;

public class ChangePasswordCommand
{
    public string Password { get; set; }
    public string ValidForgetPasswordLink { get; set; }
}