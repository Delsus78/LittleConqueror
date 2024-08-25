namespace LittleConqueror.AppService.Domain.DrivingModels.Queries;

public class GetPaginatedActionsByUserIdQuery
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}