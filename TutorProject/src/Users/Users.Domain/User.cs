using CSharpFunctionalExtensions;
using Shared.Errors;

namespace Users.Domain;

public class User
{
    private User(Guid id, string email, string password)
    {
        Id = id;
        Email = email;
        Password = password;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public List<Role> Roles { get; set; } = [];

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
}