using Microsoft.Extensions.DependencyInjection;
using Shared.Abstractions;

namespace Users.Infrastructure.Postgres;

public class UsersSeeder(IServiceScopeFactory serviceScopeFactory) : IAutoSeeder
{
    public async Task SeedAsync()
    {
        using var scope = serviceScopeFactory.CreateScope();

        var seedingService = scope.ServiceProvider.GetRequiredService<UsersSeedingService>();
        await seedingService.SeedAsync();
    }
}