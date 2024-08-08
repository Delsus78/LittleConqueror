namespace LittleConqueror.API.Models.Dtos;

public class UserInformationsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? TotalPopulation { get; set; }
    public int? TotalCities { get; set; }
    public ResourcesDto? Resources { get; set; }
}