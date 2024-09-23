namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetPaginatedActionsByUserIdQuery
{
    public long UserId { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}