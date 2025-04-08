using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Users.Domain.Roles;
using Users.Domain.ValueObjects;

namespace Users.Domain;

public class User
{
    // EF Core
    private User()
    {
    }

    private User(
        UserId id,
        Email email,
        string passwordHash,
        string passwordSalt,
        IEnumerable<Role> roles)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Roles = roles.ToList();
    }

    public UserId Id { get; set; } = null!;

    public Email Email { get; set; } = null!;

    public string PasswordHash { get; private set; } = string.Empty;

    public string PasswordSalt { get; private set; } = string.Empty;

    public List<Role> Roles { get; private set; } = [];

    public static Result<User, ErrorList> CreateUser(
        Email email,
        string passwordHash,
        string salt,
        IEnumerable<Role> roles)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Errors.General.ValueIsInvalid(nameof(passwordHash)).ToErrorList();
        }

        return new User(Guid.NewGuid(), email, passwordHash, salt, roles);
    }

    public Result<UserId, ErrorList> ChangePassword(string newPasswordHash, string newPasswordSalt)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            return Errors.General.ValueIsInvalid(nameof(newPasswordHash)).ToErrorList();

        if (string.IsNullOrWhiteSpace(newPasswordHash))
            return Errors.General.ValueIsInvalid(nameof(newPasswordSalt)).ToErrorList();

        PasswordHash = newPasswordHash;
        PasswordSalt = newPasswordSalt;

        return Id;
    }

    public Result<UserId, ErrorList> ChangeEmail(Email newEmail)
    {
        Email = newEmail;
        return Id;
    }

    public void AssignRole(Role role)
    {
        if (Roles.All(r => r.Name != role.Name))
            Roles.Add(role);
    }
}