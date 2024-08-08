namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetUserInformationsQuery
{
    public int UserId { get; set; }
    public bool IncludeTerritory { get; set; } = true;
    public bool IncludeResources { get; set; } = true;
}