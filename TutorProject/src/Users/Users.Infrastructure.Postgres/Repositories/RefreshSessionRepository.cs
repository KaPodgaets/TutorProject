using CSharpFunctionalExtensions;
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

    public Task<Result<RefreshSession, Error>> GetByRefreshToken(
        Guid refreshToken,
        CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    public void Delete(RefreshSession refreshSession) => throw new NotImplementedException();
}