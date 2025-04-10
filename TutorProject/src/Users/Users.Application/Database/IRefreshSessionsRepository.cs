using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Users.Domain;

namespace Users.Application.Database;

public interface IRefreshSessionsRepository
{
    Task<Result<RefreshSession, Error>> GetByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);

    void Delete(RefreshSession refreshSession);
}