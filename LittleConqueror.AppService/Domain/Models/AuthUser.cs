namespace LittleConqueror.AppService.Domain.Models;

public class AuthUser
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Hash { get; set; }
    public string Role { get; init; }
    
    public User User { get; set; }
}