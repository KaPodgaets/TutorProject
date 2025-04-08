using TutorProject.Application.Database;
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

    public Task<Permission?> GetPermissionByCode(string code) => throw new NotImplementedException();

    public Task<IEnumerable<Permission>?> GetAllPermissions(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<IEnumerable<string>> GetAllExistingPermissionsCodes(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task AddRange(IEnumerable<Permission> permissions, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<HashSet<string>>
        GetPermissionCodesByUserId(Guid userId, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task AddRolesWithPermissions(
        IEnumerable<Role> rolesWithPermissions,
        CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public Task ClearRolesAndPermissions(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();
}