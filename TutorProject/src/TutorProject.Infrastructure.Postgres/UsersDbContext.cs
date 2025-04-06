using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TutorProject.Domain.Users;

namespace TutorProject.Infrastructure.Postgres;

public class UsersDbContext(string connectionString) : DbContext
{
    public DbSet<User> Users => Set<User>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(UsersDbContext).Assembly,
            type => type.FullName?.Contains("Users.Configurations") ?? false);

        modelBuilder.HasDefaultSchema("users");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}