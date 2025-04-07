using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Users.Infrastructure.Postgres.DbContext;

namespace Users.Infrastructure.Postgres.Migrator;

public class UsersMigrator(
    UsersDbContext context,
    ILogger<UsersMigrator> logger) : IMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Started applying users migrations...");

        if (await context.Database.CanConnectAsync(cancellationToken) is false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        await context.Database.MigrateAsync(cancellationToken);

        logger.Log(LogLevel.Information, "Users migrations were applied successfully.");
    }
}