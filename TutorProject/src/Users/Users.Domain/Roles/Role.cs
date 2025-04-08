namespace Users.Domain.Roles;

public class Role
{
    public const string ADMIN = "Admin";
    public const string VIEWER = "Viewer";
    public const string MANAGER = "Manager";

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Permission> Permissions { get; set; } = [];
}