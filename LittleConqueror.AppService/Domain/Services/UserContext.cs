namespace LittleConqueror.AppService.Domain.Services;

public interface IUserContext
{
    long UserId { get; set; }
}

public class UserContext : IUserContext
{
    public long UserId { get; set; }
}