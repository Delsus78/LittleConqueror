namespace LittleConqueror.Infrastructure.Entities.DatabaseEntities;

public class AuthUserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Hash { get; set; }
    public string Role { get; set; }
    
    public int? UserId { get; set; }
    public UserEntity? User { get; set; }
}