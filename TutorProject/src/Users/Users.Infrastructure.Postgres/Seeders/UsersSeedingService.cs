using System.Security.Authentication;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.ValueObjects;
using TutorProject.Application.Abstractions;
using TutorProject.Application.Database;
using Users.Domain;
using Users.Domain.Roles;
using Users.Infrastructure.Postgres.Options;

namespace Users.Infrastructure.Postgres.Seeders;

public class UsersSeedingService
{
    private readonly AdminOptions _adminOptions;
    private readonly IUsersRepository _userRepository;
    private readonly IRolesRepository _rolesRepository;
    private readonly ILogger<UsersSeedingService> _logger;
    private readonly IUserManager _userManager;

    public UsersSeedingService(
        IOptions<AdminOptions> adminOptions,
        IUsersRepository userRepository,
        IRolesRepository rolesRepository,
        ILogger<UsersSeedingService> logger,
        IUserManager userManager)
    {
        _adminOptions = adminOptions.Value;
        _userRepository = userRepository;
        _rolesRepository = rolesRepository;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task SeedAsync(CancellationToken stoppingToken = default)
    {
        _logger.LogInformation("AutoSeeder: Seeding accounts...");

        RolesPermissionsToSeed seedData = await GetPermissionsToSeed(stoppingToken);

        await SeedPermissions(seedData.Permissions);

        await SeedRolesPermissionsRelationship(seedData.Roles, stoppingToken);

        await SeedAdminUser(stoppingToken);

        _logger.LogInformation("AutoSeeder: Stop seeding accounts");
    }

    private static async Task<RolesPermissionsToSeed> GetPermissionsToSeed(CancellationToken stoppingToken)
    {
        string json = await File.ReadAllTextAsync("etc/accounts.json", stoppingToken);

        var seedData = JsonSerializer.Deserialize<RolesPermissionsToSeed>(json)
                       ?? throw new ApplicationException("Could not deserialize roles and permissions to seed.");
        return seedData;
    }

    private async Task SeedPermissions(Dictionary<string, string[]> permissions)
    {
        await _rolesRepository.ClearRolesAndPermissions();

        var permissionEntities = permissions
            .SelectMany(x => x.Value.Select(y => new Permission { Code = y }));

        await _rolesRepository.AddRange(permissionEntities);
    }

    private async Task SeedRolesPermissionsRelationship(
        Dictionary<string, string[]> roles,
        CancellationToken cancellationToken = default)
    {
        var existingPermissions = await _rolesRepository.GetAllPermissions(cancellationToken);
        if (existingPermissions is null)
            throw new ApplicationException("Could not find permissions in database");

        var materializedPermissions = existingPermissions.ToList();
        List<Role> rolesEntities = [];

        foreach (var role in roles)
        {
            Role roleEntity = new() { Name = role.Key };

            foreach (string permission in role.Value)
            {
                Permission permissionEntity = materializedPermissions.First(x => x.Code == permission);
                roleEntity.Permissions.Add(permissionEntity);
            }

            rolesEntities.Add(roleEntity);
        }

        await _rolesRepository.AddRolesWithPermissions(rolesEntities, cancellationToken);
    }

    private async Task SeedAdminUser(CancellationToken stoppingToken = default)
    {
        _logger.LogInformation("AutoSeeder: Checking for existing admin user");
        if (string.IsNullOrWhiteSpace(_adminOptions.Email))
            throw new ArgumentNullException(_adminOptions.Email);
        if (string.IsNullOrWhiteSpace(_adminOptions.Password))
            throw new ArgumentNullException(_adminOptions.Password);

        var email = Email.Create(_adminOptions.Email).Value;
        var creatingPasswordResult = Password.Create(_adminOptions.Password);
        if (creatingPasswordResult.IsFailure)
            throw new AuthenticationException("Invalid email or password while seeding");

        var existingAdminResult = _userRepository.GetByEmail(email, stoppingToken).Result;
        if (existingAdminResult.IsSuccess)
        {
            _logger.LogInformation("AutoSeeder: No need to seed admin user");
            return;
        }

        var registerNewUserResult = await _userManager.RegisterNewUserAsync(
            email,
            creatingPasswordResult.Value,
            stoppingToken);

        if (registerNewUserResult.IsFailure)
            throw new Exception($"AutoSeeder: Could not create admin user: {registerNewUserResult.Error}");

        _logger.LogInformation("AutoSeeder: Admin user were created successfully");
    }
}