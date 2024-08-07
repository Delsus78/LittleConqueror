using LittleConqueror.AppService.DrivenPorts;
using Microsoft.EntityFrameworkCore.Storage;

namespace LittleConqueror.Infrastructure.DatabaseAdapters;

public class TransactionManagerAdapter(DataContext applicationDbContext) : ITransactionManagerPort
{
    private IDbContextTransaction _dbContextTransaction;
    public async Task BeginTransaction() => _dbContextTransaction = await applicationDbContext.Database.BeginTransactionAsync();
    public async Task CommitTransaction() => await _dbContextTransaction.CommitAsync();
    public async Task RollbackTransaction() => await _dbContextTransaction.RollbackAsync();
}