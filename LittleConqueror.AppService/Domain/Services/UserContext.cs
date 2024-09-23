namespace LittleConqueror.AppService.Domain.Services;

public interface IUserContext
{
    long UserId { set; }
    bool IsUnauthorized(long userId);
}

public class UserContext : IUserContext
{
    private long? _userId;

    public long UserId
    {
        set => _userId = value;
    }
    
    /// <summary>
    /// admin => userId < 0
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public bool IsUnauthorized(long userId) => _userId > 0 && _userId != userId;
}