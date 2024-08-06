using LittleConqueror.AppService.DrivenPorts;

namespace LittleConqueror.Infrastructure.JwtAdapters;

public class PasswordHasherAdapter : IPasswordHasherPort
{
    public string EnhancedHashPassword(string password) 
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password, 13);

    public bool VerifyHashedPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}