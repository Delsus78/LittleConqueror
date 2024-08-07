using LittleConqueror.AppService.Domain.Models.Entities.Base;

namespace LittleConqueror.AppService.Domain.Models.Entities;

public class AuthUser : Entity
{
    public string Username { get; set; }
    public string Hash { get; set; }
    public string Role { get; init; }
    
    // 1:1 relationship
    public int? UserId { get; set; }
    public User? User { get; set; }
}