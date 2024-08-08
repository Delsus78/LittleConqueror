namespace LittleConqueror.AppService.Domain.Models;

public class UserInformations
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? TotalPopulation { get; set; }
    public int? TotalCities { get; set; }
}