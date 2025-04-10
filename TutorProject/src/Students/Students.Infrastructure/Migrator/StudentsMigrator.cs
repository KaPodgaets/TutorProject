using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Students.Infrastructure.DbContext;

namespace Students.Infrastructure.Migrator;

public class StudentsMigrator(
    StudentsReadDbContext context,
    ILogger<StudentsMigrator> logger) : IMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Started applying students migrations...");

        if (await context.Database.CanConnectAsync(cancellationToken) is false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        await context.Database.MigrateAsync(cancellationToken);

        logger.Log(LogLevel.Information, "Students migrations were applied successfully.");
    }
}