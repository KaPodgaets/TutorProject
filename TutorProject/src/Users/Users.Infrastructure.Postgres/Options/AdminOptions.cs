namespace Users.Infrastructure.Postgres.Options;

public class AdminOptions
{
    public const string ADMIN_ROLE = "Admin";

    public string? Email { get; set; }

    public string? Password { get; set; }
}