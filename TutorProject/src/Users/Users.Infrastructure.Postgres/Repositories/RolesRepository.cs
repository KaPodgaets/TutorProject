using Microsoft.EntityFrameworkCore;
using Users.Application.Database;
using Users.Domain;
using Users.Domain.Roles;
using Users.Infrastructure.Postgres.DbContext;

namespace Users.Infrastructure.Postgres.Repositories;

public class RolesRepository : IRolesRepository
{
    private readonly UsersDbContext _dbContext;

    public RolesRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Permission?> GetPermissionByCode(string code)
        => await _dbContext.Permissions.FirstOrDefaultAsync(p => p.Code == code);

    public async Task<IEnumerable<Permission>?> GetAllPermissions(CancellationToken cancellationToken = default)
        => await _dbContext.Permissions.ToListAsync(cancellationToken);

    public async Task<IEnumerable<string>> GetAllExistingPermissionsCodes(CancellationToken cancellationToken = default)
        => await _dbContext.Permissions.Select(p => p.Code).ToListAsync(cancellationToken: cancellationToken);

    public async Task AddRange(IEnumerable<Permission> permissions, CancellationToken cancellationToken = default)
    {
        await _dbContext.Permissions.AddRangeAsync(permissions, cancellationToken);
    }

    public async Task<HashSet<string>> GetPermissionCodesByUserId(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var perms = await _dbContext.Users
            .Include(u => u.Roles)
            .ThenInclude(r => r.Permissions)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.Permissions)
            .Select(p => p.Code)
            .ToHashSetAsync(cancellationToken);

        return perms;
    }

    public async Task AddRolesWithPermissions(
        IEnumerable<Role> rolesWithPermissions,
        CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.AddRangeAsync(rolesWithPermissions, cancellationToken);
    }

    public async Task ClearRolesAndPermissions(CancellationToken cancellationToken = default)
    {
        await _dbContext.Roles.ExecuteDeleteAsync(cancellationToken);
        await _dbContext.Permissions.ExecuteDeleteAsync(cancellationToken);
    }

    public async Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == name, cancellationToken: cancellationToken);
    }
}