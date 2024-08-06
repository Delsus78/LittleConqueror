namespace LittleConqueror.AppService.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Territory Territory { get; set; }
    
    public AuthUser AuthUser { get; set; }
}