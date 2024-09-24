namespace LittleConqueror.AppService.DrivenPorts;

public interface IBackgroundJobIdentifiersDatabasePort
{
    Task<string?> GetBackgroundJobIdentifierForNaturalId(string naturalId);
    Task SetBackgroundJobIdentifierForNaturalId(string naturalId, string identifier);
    Task RemoveBackgroundJobIdentifierForNaturalId(string naturalId);
}