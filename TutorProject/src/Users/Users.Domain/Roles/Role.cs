namespace Users.Domain.Roles;

public class Role
{
    public Role(string name)
    {
        Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public List<Permission> Permissions { get; set; } = [];
}