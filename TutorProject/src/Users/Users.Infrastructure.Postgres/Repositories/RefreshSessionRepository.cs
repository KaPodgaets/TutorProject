using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shared.ResultPattern;
using TutorProject.Application.Database;
using Users.Domain;
using Users.Infrastructure.Postgres.DbContext;

namespace Users.Infrastructure.Postgres.Repositories;

public class RefreshSessionRepository : IRefreshSessionsRepository
{
    private readonly UsersDbContext _dbContext;

    public RefreshSessionRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken, CancellationToken cancellationToken)
    {
        var refreshSession = await _dbContext.RefreshSessions
            .Include(r => r.User)
            .ThenInclude(u => u.Roles)
            .FirstOrDefaultAsync(r => r.RefreshToken == refreshToken, cancellationToken);

        if (refreshSession is null)
            return Errors.General.NotFound(refreshToken);

        return refreshSession;
    }

    public void Delete(RefreshSession refreshSession)
    {
        _dbContext.RefreshSessions.Remove(refreshSession);
    }
}