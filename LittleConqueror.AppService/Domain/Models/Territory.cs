namespace LittleConqueror.AppService.Domain.Models;

public class Territory
{
    public int Id { get; set; }
    
    public User Owner { get; set; }
    public IEnumerable<City> Cities { get; set; }
}