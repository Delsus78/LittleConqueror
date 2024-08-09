namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetUserInformationsQuery
{
    public long UserId { get; set; }
    public bool IncludeTerritory { get; set; } = true;
    public bool IncludeResources { get; set; } = true;
}