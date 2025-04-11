using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.Database;

namespace Tutors.Infrastructure.DbContext;

internal class UnitOfWork : IUnitOfWork
{
    private readonly TutorsDbContext _tutorsDbContext;

    public UnitOfWork(TutorsDbContext tutorsDbContext)
    {
        _tutorsDbContext = tutorsDbContext;
    }

    public async Task<DbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _tutorsDbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _tutorsDbContext.SaveChangesAsync(cancellationToken);
    }
}