using System.Security.Cryptography;
using System.Text;
using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using TutorProject.Application.Abstractions;
using TutorProject.Application.Database;
using Users.Domain;
using Users.Domain.ValueObjects;

namespace Users.Infrastructure.Postgres.Managers;

public class UserManager : IUserManager
{
    // private readonly IUserRepository _userRepository;
    private readonly IUsersRepository _userRepository;

    public UserManager(IUsersRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public (string Hash, string Salt) HashPassword(string password)
    {
        using var hmac = new HMACSHA512();
        string salt = Convert.ToBase64String(hmac.Key);
        string hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return (hash, salt);
    }

    public bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);
        using var hmac = new HMACSHA512(saltBytes);
        string computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return computedHash == storedHash;
    }

    public async Task<Result<UserId, ErrorList>>
        LoginAsync(Email email, string password, CancellationToken cancellationToken)
    {
        var getUserResult = await _userRepository.GetByEmail(email, cancellationToken);
        if (getUserResult.IsFailure)
            return Errors.Auth.InvalidCredentials().ToErrorList();
        var user = getUserResult.Value;

        if (VerifyPassword(user.PasswordHash, user.PasswordSalt, password) is false)
            return Errors.Auth.InvalidCredentials().ToErrorList();

        return user.Id;
    }

    public async Task<Result<User, ErrorList>> RegisterNewUserAsync(
        Email email,
        Password password,
        CancellationToken cancellationToken)
    {
        var (hash, salt) = HashPassword(password.Value);
        var createUserResult = User.CreateUser(email, hash, salt);
        if (createUserResult.IsFailure)
            return Errors.Auth.InvalidCredentials().ToErrorList();

        var saveResult = await _userRepository.Create(createUserResult.Value, cancellationToken);
        if (saveResult.IsFailure)
            return Errors.General.Failure().ToErrorList();
        return saveResult.Value;
    }

    public Task LogoutAsync(UserId userId) => throw new NotImplementedException();
}