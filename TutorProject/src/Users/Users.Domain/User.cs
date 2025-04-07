using CSharpFunctionalExtensions;
using Shared.Errors;

namespace Users.Domain;

public class User
{
    private User(Guid id, string email, string passwordHash)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public List<Role> Roles { get; private set; } = new();

    public static Result<User, ErrorList> CreateUser(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Errors.General.ValueIsInvalid(nameof(email)).ToErrorList();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            return Errors.General.ValueIsInvalid(nameof(password)).ToErrorList();
        }

        return new User(Guid.NewGuid(), email, password);
    }

    public Result<Guid, ErrorList> ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            return Errors.General.ValueIsInvalid(nameof(newPasswordHash)).ToErrorList();

        PasswordHash = newPasswordHash;
        return Id;
    }

    public Result<Guid, ErrorList> ChangeEmail(string newEmail)
    {
        if (string.IsNullOrWhiteSpace(newEmail))
            return Errors.General.ValueIsInvalid(nameof(newEmail)).ToErrorList();

        Email = newEmail;
        return Id;
    }

    public void AssignRole(Role role)
    {
        if (Roles.All(r => r.Name != role.Name))
            Roles.Add(role);
    }
}