using CSharpFunctionalExtensions;
using Shared.ResultPattern;
using Shared.ValueObjects;
using Users.Domain.ValueObjects;

namespace Users.Domain;

public class User
{
    private User(Guid id, Email email, string passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
    }

    public UserId Id { get; set; }

    public Email Email { get; set; }

    public string PasswordHash { get; set; }

    public List<Role> Roles { get; private set; } = [];

    public static Result<User, ErrorList> CreateUser(Email email, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            return Errors.General.ValueIsInvalid(nameof(password)).ToErrorList();
        }

        return new User(Guid.NewGuid(), email, password);
    }

    public Result<UserId, ErrorList> ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            return Errors.General.ValueIsInvalid(nameof(newPasswordHash)).ToErrorList();

        PasswordHash = newPasswordHash;
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