using LittleConqueror.AppService.Domain.Models.Entities;

namespace LittleConqueror.AppService.Domain.Models;

public class UserInformations
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int? TotalPopulation { get; set; }
    public int? TotalCities { get; set; }
    public Resources? Resources { get; set; }
    public double TotalHappiness { get; set; }
}