using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Database;

namespace Users.Infrastructure.Postgres.DbContext;

internal class UnitOfWork : IUnitOfWork
{
    private readonly UsersDbContext _accountsDbContext;

    public UnitOfWork(UsersDbContext usersDbContext)
    {
        _accountsDbContext = usersDbContext;
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