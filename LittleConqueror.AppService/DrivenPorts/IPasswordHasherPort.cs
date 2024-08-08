namespace LittleConqueror.AppService.DrivenPorts;

public interface IPasswordHasherPort
{
    string EnhancedHashPassword(string password);
    bool VerifyHashedPassword(string password, string hashedPassword);
}