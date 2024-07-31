namespace LittleConqueror.AppService.DomainEntities;

public class Territory
{
    public int Id { get; set; }
    
    public List<City> Cities { get; set; }
    public User User { get; set; }
}