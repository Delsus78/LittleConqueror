using LittleConqueror.AppService.Exceptions;

namespace LittleConqueror.AppService.Domain.Services;

public interface IUserContext
{
    long UserId { get; set; }
}

public class UserContext : IUserContext
{
    private long? _userId;

    public long UserId
    {
        get => _userId ?? throw new AppException("User Id is not set", 404);
        set => _userId = value;
    }
}