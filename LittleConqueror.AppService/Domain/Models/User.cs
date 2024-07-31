namespace LittleConqueror.AppService.DomainEntities;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    
    public Territory? Territory { get; set; }
}