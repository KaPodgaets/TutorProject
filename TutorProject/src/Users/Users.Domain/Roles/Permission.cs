using Users.Domain.Roles;

namespace Users.Domain;

public class Permission
{
    public Permission(Guid id, string code)
    {
        Id = id;
        Code = code;
    }

    public Guid Id { get; set; }

    public string Code { get; set; }

    public ICollection<Role> Roles { get; set; } = [];
}