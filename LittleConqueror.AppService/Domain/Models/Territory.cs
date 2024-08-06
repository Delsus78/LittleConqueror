namespace LittleConqueror.AppService.Domain.Models;

public class Territory
{
    public int Id { get; set; }
    
    public User Owner { get; set; }
    public List<City> Cities { get; set; }
}