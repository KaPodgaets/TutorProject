using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Users.Domain.Roles;
using Users.Domain.ValueObjects;

namespace Users.Domain;

public class User
{
    private User(
        UserId id,
        Email email,
        string passwordHash,
        string passwordSalt)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public UserId Id { get; set; }

    public Email Email { get; set; }

    public string PasswordHash { get; private set; }

    public string PasswordSalt { get; private set; }

    public List<Role> Roles { get; private set; } = [];

    public static Result<User, ErrorList> CreateUser(Email email, string passwordHash, string salt)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Errors.General.ValueIsInvalid(nameof(passwordHash)).ToErrorList();
        }

        return new User(Guid.NewGuid(), email, passwordHash, salt);
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