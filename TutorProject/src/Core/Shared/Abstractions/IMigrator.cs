namespace Shared.Abstractions;

public interface IMigrator
{
    public Task MigrateAsync(CancellationToken cancellationToken = default);
}