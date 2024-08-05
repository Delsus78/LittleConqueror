namespace LittleConqueror.AppService.Exceptions;

public class AppException(string message, int errorCode) : Exception(message)
{
    public int ErrorCode { get; } = errorCode;
}