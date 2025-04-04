namespace TutorProject.Domain.Users;

public class User
{
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public Guid Id { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}