using System.Security.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.ValueObjects;
using TutorProject.Application.Abstractions;
using TutorProject.Application.Database;
using Users.Infrastructure.Postgres.Options;

namespace Users.Infrastructure.Postgres.Seeders;

public class UsersSeedingService
{
    private readonly AdminOptions _adminOptions;
    private readonly IUsersRepository _repository;
    private readonly ILogger<UsersSeedingService> _logger;
    private readonly IUserManager _userManager;

    public UsersSeedingService(
        IOptions<AdminOptions> adminOptions,
        IUsersRepository repository,
        ILogger<UsersSeedingService> logger,
        IUserManager userManager)
    {
        _adminOptions = adminOptions.Value;
        _repository = repository;
        _logger = logger;
        _userManager = userManager;
    }

    public async Task SeedAsync(CancellationToken stoppingToken = default)
    {
        _logger.LogInformation("AutoSeeder: Seeding accounts...");
        await SeedAdminUser(stoppingToken);
        _logger.LogInformation("AutoSeeder: Stop seeding accounts");
    }

    private async Task SeedAdminUser(CancellationToken stoppingToken = default)
    {
        _logger.LogInformation("AutoSeeder: Checking for existing admin user");
        if (string.IsNullOrWhiteSpace(_adminOptions.Email))
            throw new ArgumentNullException(nameof(_adminOptions.Email));
        if (string.IsNullOrWhiteSpace(_adminOptions.Password))
            throw new ArgumentNullException(nameof(_adminOptions.Password));

        var email = Email.Create(_adminOptions.Email).Value;
        var creatingPasswordResult = Password.Create(_adminOptions.Password);
        if (creatingPasswordResult.IsFailure)
            throw new AuthenticationException("Invalid email or password while seeding");

        var existingAdminResult = _repository.GetByEmail(email, stoppingToken).Result;
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