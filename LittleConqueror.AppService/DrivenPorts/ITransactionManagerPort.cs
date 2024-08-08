namespace LittleConqueror.AppService.DrivenPorts;

public interface ITransactionManagerPort
{
    Task BeginTransaction();
    Task CommitTransaction();
    Task RollbackTransaction();
}