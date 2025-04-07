using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.ValueObjects;
using TutorProject.Application.Database;
using Users.Domain;

namespace Users.Infrastructure.Postgres.Seeders;

public class UsersSeedingService
{
    private readonly AdminOptions _adminOptions;
    private readonly IUsersRepository _repository;
    private readonly ILogger<UsersSeedingService> _logger;

    public UsersSeedingService(
        IOptions<AdminOptions> adminOptions,
        IUsersRepository repository,
        ILogger<UsersSeedingService> logger)
    {
        _adminOptions = adminOptions.Value;
        _repository = repository;
        _logger = logger;
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

        var existingAdminResult = _repository.GetByEmail(email, stoppingToken).Result;
        if (existingAdminResult.IsSuccess)
        {
            _logger.LogInformation("AutoSeeder: No need to seed admin user");
            return;
        }

        var creatingUserResult = User.CreateUser(email, _adminOptions.Password);
        if (creatingUserResult.IsFailure)
            throw new Exception($"AutoSeeder: Could not create admin user: {creatingUserResult.Error}");

        var savingUserResult = await _repository.Create(creatingUserResult.Value, stoppingToken);
        if (savingUserResult.IsFailure)
            throw new Exception($"AutoSeeder: Could not save admin user: {savingUserResult.Error}");

        _logger.LogInformation("AutoSeeder: Admin user were created successfully");
    }
}