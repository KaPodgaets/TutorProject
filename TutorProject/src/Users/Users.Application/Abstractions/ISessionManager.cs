using Users.Domain;
using Users.Domain.ValueObjects;

namespace TutorProject.Application.Abstractions;

public interface ISessionManager
{
    Task StartSessionAsync(UserId userId, CancellationToken cancellationToken);

    Task EndSessionAsync(UserId userId);

    Task<Session?> GetSessionByUserIdAsync(UserId userId);
}