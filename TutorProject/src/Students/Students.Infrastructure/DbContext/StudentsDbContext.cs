using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Students.Domain.Schools;
using Students.Domain.Students;

namespace Students.Infrastructure.DbContext;

public class StudentsDbContext(string connectionString) : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Student> Students => Set<Student>();

    public DbSet<Parent> RefreshSessions => Set<Parent>();

    public DbSet<School> Roles => Set<School>();

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
            typeof(StudentsDbContext).Assembly,
            type => type.FullName?.Contains("Configurations") ?? false);

        modelBuilder.HasDefaultSchema("students");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}