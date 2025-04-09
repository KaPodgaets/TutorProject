namespace Users.Domain.Roles;

public class RolePermission
{
    public RolePermission(Guid roleId, Role role, Guid permissionId, Permission permission)
    {
        RoleId = roleId;
        Role = role;
        PermissionId = permissionId;
        Permission = permission;
    }

    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    public Guid PermissionId { get; set; }

    public Permission Permission { get; set; }
}