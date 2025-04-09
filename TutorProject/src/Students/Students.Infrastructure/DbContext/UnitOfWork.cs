using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Database;

namespace Students.Infrastructure.DbContext;

internal class UnitOfWork : IUnitOfWork
{
    private readonly StudentsDbContext _accountsDbContext;

    public UnitOfWork(StudentsDbContext studentsDbContext)
    {
        _accountsDbContext = studentsDbContext;
    }

    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _accountsDbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _accountsDbContext.SaveChangesAsync(cancellationToken);
    }
}