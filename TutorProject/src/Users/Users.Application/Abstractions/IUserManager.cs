using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Users.Domain;
using Users.Domain.Roles;
using Users.Domain.ValueObjects;

namespace TutorProject.Application.Abstractions;

public interface IUserManager
{
    (string Hash, string Salt) HashPassword(string password);

    bool VerifyPassword(string password, string storedHash, string storedSalt);

    Task<Result<UserId, ErrorList>> LoginAsync(Email email, string password, CancellationToken cancellationToken);

    Task LogoutAsync(UserId userId);

    Task<Result<User, ErrorList>> RegisterNewUserAsync(
        Email email,
        Password password,
        IEnumerable<Role> roles,
        CancellationToken cancellationToken);
}