namespace LittleConqueror.API.Models.Dtos;

public class GetUserInformationsQueryDto
{
    public bool IncludeTerritory { get; set; } = true;
    public bool IncludeResources { get; set; } = true;
}