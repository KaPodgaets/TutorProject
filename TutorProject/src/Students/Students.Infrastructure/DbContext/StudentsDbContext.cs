using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Students.Application.Database;
using Students.Domain.Schools;
using Students.Domain.Students;

namespace Students.Infrastructure.DbContext;

public class StudentsDbContext(string connectionString) : Microsoft.EntityFrameworkCore.DbContext, IStudentsDbContext
{
    // Write
    public DbSet<Student> Students => Set<Student>();

    public DbSet<Parent> Parents => Set<Parent>();

    public DbSet<School> Schools => Set<School>();

    // Queries
    IQueryable<Parent> IStudentsDbContext.Parents => Parents.AsQueryable();

    IQueryable<Student> IStudentsDbContext.Students => Students.AsQueryable();

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