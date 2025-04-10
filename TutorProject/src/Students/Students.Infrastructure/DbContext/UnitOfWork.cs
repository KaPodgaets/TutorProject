using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Database;

namespace Students.Infrastructure.DbContext;

internal class UnitOfWork : IUnitOfWork
{
    private readonly StudentsReadDbContext _accountsReadDbContext;

    public UnitOfWork(StudentsReadDbContext studentsReadDbContext)
    {
        _accountsReadDbContext = studentsReadDbContext;
    }

    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _accountsReadDbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _accountsReadDbContext.SaveChangesAsync(cancellationToken);
    }
}