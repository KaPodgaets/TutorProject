using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.Abstractions;
using Tutors.Infrastructure.DbContext;

namespace Tutors.Infrastructure.Migrator;

public class TutorsMigrator(
    TutorsDbContext context,
    ILogger<TutorsMigrator> logger) : IMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Started applying tutors migrations...");

        if (await context.Database.CanConnectAsync(cancellationToken) is false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        await context.Database.MigrateAsync(cancellationToken);

        logger.Log(LogLevel.Information, "Tutors migrations were applied successfully.");
    }
}